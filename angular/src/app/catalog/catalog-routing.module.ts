import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product/product.component';
import { AttributeComponent } from './attribute/attribute.component';
import { PermissionGuard } from '@abp/ng.core';

const routes: Routes = [
  { path: 'product', component: ProductComponent, canActivate: [PermissionGuard], data: {
    requirePolicy: 'BMHEcomAdminCatalog.Product',
  } },
  { path: 'attribute', component: AttributeComponent, canActivate: [PermissionGuard], data: {
    requirePolicy: 'BMHEcomAdminCatalog.Attribute',
  } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
