import { MessageService } from 'primeng/api';
import { Injectable } from "@angular/core";

@Injectable()

export class NotificationService {
    constructor(private messageService : MessageService)
    {

    }

    showSuccess(message : string)
    {
        this.messageService.add({ severity: 'success', summary: 'Thành công', detail : message });
    }

    showError(message : string)
    {
        this.messageService.add({ severity: 'error', summary: 'Lỗi', detail : message });
    }
}