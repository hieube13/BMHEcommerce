import { NotificationService } from 'src/app/shared/Services/notification.service'; 
import { DialogService } from 'primeng/dynamicdialog';
import { ProductAttributeService } from '@proxy/system/catalog/product-attributes';  
import { AuthService, PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductAttributeDto, ProductAttributeInListDto } from '@proxy/catalog/product-attributes'; 
import { Subject, takeUntil } from 'rxjs';
import { AttributeDetailComponent } from './attribute-detail.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AttributeType } from '@proxy/bmhecommerce/product-attributes';

@Component({
  selector: 'app-attribute',
  templateUrl: './attribute.component.html',
  styleUrls: ['./attribute.component.scss'],
})
export class AttributeComponent  implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel:boolean = false;
  items: ProductAttributeInListDto[] = []
  public selectedItems: ProductAttributeInListDto[] = [];

  //Paging variables
  public skipCount = 0;
  public maxResultCount = 10;
  public totalCount: number;

  //filter
  productCategories: any[] = [];
  keyword: string = '';
  categoryId: string = '';

  constructor(
      private productAttributeService : ProductAttributeService, 
      private dialogService : DialogService,
      private notificationService : NotificationService,
      private confirmationService : ConfirmationService
    ) 
    {}
  
  ngOnInit(): void {
    // this.loadProductCategories();
    this.loadData();
  }
  
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete()
  }

  public loadData(): void {
    this.toggleBlockUI(true); 
    this.productAttributeService.getListFilter({
      keyword: this.keyword,
      maxResultCount: this.maxResultCount,
      skipCount: this.skipCount
    })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: ((res : PagedResultDto<ProductAttributeInListDto>) => {
        this.items = res.items;
        this.totalCount = res.totalCount;
        this.toggleBlockUI(false);
      }),
      error: () => {
        this.toggleBlockUI(false);
      }
    });
  }

  // loadProductCategories()
  // {
  //   this.productAttributeService.getListAll()
  //   .subscribe((res : ProductAttributeService[]) => {
  //     res.forEach(element => {
  //       this.productCategories.push({
  //         value: element.id,
  //         label: element.name
  //       })
  //     })
  //   });
  // }

  public pageChanged(event : any) : void {
    this.skipCount = (event.page - 1) * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  showAddModel()
  {
    const ref = this.dialogService.open(AttributeDetailComponent, {
      header: 'Thêm mới sản phẩm',
      width: '70%',
    });

    ref.onClose.subscribe((data : ProductAttributeDto) => {
      if(data){
        this.loadData();
        this.notificationService.showSuccess("Thêm thuộc tính thành công");
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
    const ref = this.dialogService.open(AttributeDetailComponent, {
      header: 'Cập nhật sản phẩm',
      width: '70%',
      data: {
        id: id
      }
    });

    ref.onClose.subscribe((data : ProductAttributeDto) => {
      if(data){
        this.loadData();
        this.selectedItems = [];
        this.notificationService.showSuccess("cập nhật thuộc tính thành công");
      }
    })
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
    this.productAttributeService.deleteMultiple(ids)
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

  getProductAttributeTypeName(value : number)
  {
    return AttributeType[value];
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
