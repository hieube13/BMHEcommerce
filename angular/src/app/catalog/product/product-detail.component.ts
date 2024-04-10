import { ManufacturersService } from '@proxy/system/catalog/manufacturers'; 
import { ProductsService } from '@proxy/catalog/products';  
import { AuthService, PagedResultDto } from '@abp/ng.core';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductCategoriesService } from '@proxy/system/catalog/product-categories'; 
import { ProductDto } from '@proxy/catalog/products'; 
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject, forkJoin, take, takeUntil } from 'rxjs';
import { ManufacturerInListDto } from '@proxy/catalog/manufacturers'; 
import { UtilityService } from 'src/app/shared/Services/utility.service'; 
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ProductType, productTypeOptions } from '@proxy/bmhecommerce/products';
import { NotificationService } from 'src/app/shared/Services/notification.service'; 
import { DomSanitizer } from '@angular/platform-browser';
import { ProductCategoryInListDto } from '@proxy/catalog/product-categories';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel:boolean = false;
  btnDisabled:boolean = false;
  manufacturers: any[] = [];
  public form : FormGroup;
  public thumbnailImage;

  //Dropdown
  productCategories: any[] = [];
  productTypes: any[] = [];
  selectedEntity = {} as ProductDto ;

  constructor(
    private productsService : ProductsService, 
    private productCategoryService : ProductCategoriesService,
    private fb : FormBuilder,
    private manufacturersService : ManufacturersService,
    private utilService : UtilityService,
    private config: DynamicDialogConfig,
    private ref: DynamicDialogRef ,
    private notificationService : NotificationService,
    private cd : ChangeDetectorRef,
    private sanitizer : DomSanitizer
    ) 
  {}
  
    validationMessages = {
      code: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
      name: [
        { type: 'required', message: 'Bạn phải nhập tên' },
        { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
      ],
      slug: [{ type: 'required', message: 'Bạn phải URL duy nhất' }],
      sku: [{ type: 'required', message: 'Bạn phải mã SKU sản phẩm' }],
      manufacturerId:[{ type: 'required', message: 'Bạn phải chọn nhà cung cấp' }],
      categoryId: [{ type: 'required', message: 'Bạn phải chọn danh mục' }],
      productType: [{ type: 'required', message: 'Bạn phải chọn loại sản phẩm' }],
      sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
      sellPrice: [{ type: 'required', message: 'Bạn phải nhập giá bán' }]
    };
  
  ngOnInit(): void {
    this.buildForm();
    this.loadProductTypes();

    this.initFormData();
  }
  
  initFormData()
  {
    //load data from form
    var productCategories = this.productCategoryService.getListAll();
    var manufacturers = this.manufacturersService.getListAll();
    this.toggleBlockUI(true);
    forkJoin({
      productCategories,
      manufacturers,
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next : (response : any) => {
        //Push data to dropdown
        var productCategories = response.productCategories as ProductCategoryInListDto[];
        var manufacturers = response.manufacturers as ManufacturerInListDto[];

        productCategories.forEach(element => {
          this.productCategories.push({
            value: element.id,
            label: element.name,
          });
        });

        manufacturers.forEach(element => {
          this.manufacturers.push({
            value: element.id,
            label: element.name,
          });
        });

        //Load edit data to form
        if (this.utilService.isEmpty(this.config.data?.id) == true) {
          this.getNewSuggestionCode();
          this.toggleBlockUI(false);
        } else {
          this.loadFormDetail(this.config.data?.id);
        }
      },
      error: () => {
        this.toggleBlockUI(false);
      },
    })
  }
  
  ngOnDestroy(): void {

    if (this.ref) {
      this.ref.close();
    }

    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete()
  }

  generateSlug()
  {
    this.form.controls['slug'].setValue(this.utilService.MakeSeoTitle(this.form.get('name').value));
  }

  getNewSuggestionCode()
  {
    this.productsService.getSuggestNewCode()
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: ((res : string) => {
        this.form.patchValue({
          code: res
        })
      })
    })
  }

  public loadFormDetail(id : string): void {
    this.toggleBlockUI(true); 
    this.productsService.get(id).pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: ((res : ProductDto) => {
        this.selectedEntity = res;
        this.loadThumbnail(this.selectedEntity.thumbnailPicture);
        this.buildForm();
        this.toggleBlockUI(false);
      }),
      error: (() => {
        this.toggleBlockUI(false);
      })
    })
  }

  saveChange()
  {

    console.log(this.form.value)
    console.log(this.config.data)
    this.toggleBlockUI(true);

    if(this.utilService.isEmpty(this.config.data?.id) == true) //Tạo mới
    {
      this.productsService.create(this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        }),
        error: ((err) => {
          this.notificationService.showError(err.error.error.message);
          this.toggleBlockUI(false);
        })
      })
    }
    else //update
    {
      this.productsService.update(this.config.data?.id, this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (() => {
          this.toggleBlockUI(false);
          this.ref.close(this.form.value);
        }),
        error: ((err) => {
          // this.notificationService.showError(err.error.error.message);
          this.toggleBlockUI(false);
        })
      })
    }
  }

  loadProductTypes() {
    productTypeOptions.forEach(element => {
      this.productTypes.push({
        value: element.value,
        label: element.key,
      });
    });
  }

  private buildForm()
  {
    this.form = this.fb.group({
      name: new FormControl(this.selectedEntity.name || null, Validators.compose([
        Validators.required,
        Validators.maxLength(250)
      ])),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
      slug: new FormControl(this.selectedEntity.slug || null, Validators.required),
      sku: new FormControl(this.selectedEntity.sku || null, Validators.required),
      manufacturerId: new FormControl(this.selectedEntity.manufacturerId || null, Validators.required),
      categoryId: new FormControl(this.selectedEntity.categoryId || null, Validators.required),
      productType: new FormControl(this.selectedEntity.productType || null, Validators.required),
      sortOrder: new FormControl(this.selectedEntity.sortOrder || null, Validators.required),
      sellPrice: new FormControl(this.selectedEntity.sellPrice || null, Validators.required),
      visiblity: new FormControl(this.selectedEntity.visiblity || true),
      isActive: new FormControl(this.selectedEntity.isActive || true),
      seoMetaDescription: new FormControl(this.selectedEntity.seoMetaDescription || null),
      description: new FormControl(this.selectedEntity.description || null),
      thumbnailPictureName: new FormControl(this.selectedEntity.description || null),
      thumbnailPictureContent: new FormControl(null),
    });
  }

  loadThumbnail(fileName : string)
  {
    this.productsService.getThumbnailImage(fileName)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: ((response : string) => {
        var fileText = this.selectedEntity.thumbnailPicture?.split('.').pop();
        this.thumbnailImage = this.sanitizer.bypassSecurityTrustResourceUrl(
          `data:image/${fileText};base64,${response}`
        )
      })
    })
  }

  private toggleBlockUI(enabled : boolean)
  {
    if(enabled == true)
    {
      this.blockedPanel = true;
      this.btnDisabled = true;
    }
    else
    {
      setTimeout(() => {
        this.blockedPanel = false;
        this.btnDisabled = false;
      }, 1000);
    }
  }

  onFileChange(event){
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.form.patchValue({
          thumbnailPictureName: file.name,
          thumbnailPictureContent: reader.result,
        });

        this.thumbnailImage = reader.result
        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

}