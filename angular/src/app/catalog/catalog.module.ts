import { BmhSharedModule } from './../shared/modules/bmh-shared.module';
import { InputTextModule } from 'primeng/inputtext';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { AttributeComponent } from './attribute/attribute.component'; 
import { AttributeDetailComponent } from './attribute/attribute-detail.component'; 
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { CatalogRoutingModule } from './catalog-routing.module';
import { ProductComponent } from './product/product.component';
import { ProductDetailComponent } from './product/product-detail.component';
import { ProductAttributeComponent } from './product/product-attribute.component';

@NgModule({
  declarations: [AttributeComponent, AttributeDetailComponent, ProductComponent, ProductDetailComponent, ProductAttributeComponent],
  imports: [
    SharedModule, 
    CatalogRoutingModule, 
    PanelModule, 
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    BmhSharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    ToastModule
  ],
  entryComponents: [
    ProductDetailComponent, ProductAttributeComponent, AttributeDetailComponent
  ]
})
export class CatalogModule {}
