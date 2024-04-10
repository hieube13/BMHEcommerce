import { ProductCategoriesService } from '@proxy/system/catalog/product-categories'; 
import { NotificationService } from 'src/app/shared/Services/notification.service'; 
import { DialogService } from 'primeng/dynamicdialog';
import { ProductsService } from '@proxy/catalog/products';  
import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductDto, ProductInListDto } from '@proxy/catalog/products'; 
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject, takeUntil } from 'rxjs';
import { ProductDetailComponent } from './product-detail.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ProductType } from '@proxy/bmhecommerce/products';
import { AttributeComponent } from '../attribute/attribute.component';
import { ProductAttributeComponent } from './product-attribute.component';
import { ProductCategoryInListDto } from '@proxy/catalog/product-categories';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent  implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel:boolean = false;
  items: ProductInListDto[] = []
  public selectedItems: ProductInListDto[] = [];

  //Paging variables
  public skipCount = 0;
  public maxResultCount = 10;
  public totalCount: number;

  //filter
  productCategories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
      private productsService : ProductsService, 
      private productCategoryService : ProductCategoriesService,
      private dialogService : DialogService,
      private notificationService : NotificationService,
      private confirmationService : ConfirmationService
    ) 
    {}
  
  ngOnInit(): void {
    this.loadProductCategories();
    this.loadData();
  }
  
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete()
  }

  public loadData(): void {
    this.toggleBlockUI(true); 
    this.productsService.getListFilter({
      keyword: this.keyword,
      categoryId: this.categoryId,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: ((res : PagedResultDto<ProductInListDto>) => {
        this.items = res.items;
        this.totalCount = res.totalCount;
        this.toggleBlockUI(false);
      }),
      error: () => {
        this.toggleBlockUI(false);
      }
    });
  }

  loadProductCategories()
  {
    this.productCategoryService.getListAll()
    .subscribe((res : ProductCategoryInListDto[]) => {
      res.forEach(element => {
        this.productCategories.push({
          value: element.id,
          label: element.name
        })
      })
    });
  }

  public pageChanged(event : any) : void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  showAddModel()
  {
    const ref = this.dialogService.open(ProductDetailComponent, {
      header: 'Thêm mới sản phẩm',
      width: '70%',
    });

    ref.onClose.subscribe((data : ProductDetailComponent) => {
      if(data){
        this.loadData();
        this.notificationService.showSuccess("Thêm sản phẩm thành công");
        this.selectedItems = [];
      }
    })
  }

  showEditModel()
  {
    if(this.selectedItems.length == 0)
    {
      this.notificationService.showError("Bạn phải chọn  một bản ghi");
      return;
    }

    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(ProductDetailComponent, {
      header: 'Cập nhật sản phẩm',
      width: '70%',
      data: {
        id: id
      }
    });

    ref.onClose.subscribe((data : ProductDetailComponent) => {
      if(data){
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess("cập nhật sản phẩm thành công");
      }
    })
  }

  manageProductAttribute(id : string)
  {
    const ref = this.dialogService.open(ProductAttributeComponent, {
      data: {
        id: id,
      },
      header: 'Quản lý thuộc tính sản phẩm',
      width: '70%',
    });

    ref.onClose.subscribe((data: ProductDto) => {
      if (data) {
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess('Cập nhật thuộc tính sản phẩm thành công');
      }
    });
  }

  deleteItems()
  {
    if(this.selectedItems.length === 0)
    {
      this.notificationService.showError("Phải chọn ít nhất 1 bản ghi");
      return;
    }

    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    })

    console.log(this.selectedItems);

    this.confirmationService.confirm({
      message: "Bạn có chắc muốn xoá",
      accept: () => {
        this.deleteItemsConfirmed(ids);
      }
    })
  }

  deleteItemsConfirmed(ids : string[])
  {
    this.toggleBlockUI(true);
    this.productsService.deleteMultiple(ids)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (() => {
        this.notificationService.showSuccess("Xoá thành công");
        this.loadData();
        this.selectedItems = [];
        this.toggleBlockUI(false);
      }),
      error: (() => {
        this.toggleBlockUI(false);
      })
    })
  }

  getProductTypeName(value : number)
  {
    return ProductType[value];
  }

  private toggleBlockUI(enabled : boolean)
  {
    if(enabled == true)
    {
      this.blockedPanel = true;
    }
    else
    {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }
}
