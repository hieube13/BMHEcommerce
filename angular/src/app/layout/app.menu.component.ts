import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    constructor(public layoutService: LayoutService) { }

    ngOnInit() {
        this.model = [
            {
                label: 'Home',
                arr: [
                    { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }
                ]
            },
            {
                label: 'Product',
                arr: [
                    { label: 'Danh sách sản phẩm', icon: 'pi pi-fw pi-home', routerLink: ['/catalog/product'], permission: 'BMHEcomAdminCatalog.Product' },
                    { label: 'Danh sách thuộc tính', icon: 'pi pi-fw pi-home', routerLink: ['/catalog/attribute'], permission: 'BMHEcomAdminCatalog.Attribute' }
                ]
            },
            {
                label: 'Hệ thống',
                arr: [
                    { label: 'Danh sách phân quyền', icon: 'pi pi-fw pi-home', routerLink: ['/system/role'], permission: 'AbpIdentity.Roles' },
                    { label: 'Danh sách người dùng', icon: 'pi pi-fw pi-home', routerLink: ['/system/user'], permission: 'AbpIdentity.Users' }
                ]
            },
        ];
    }
}
