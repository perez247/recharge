import { ToasterService } from './../_services/toaster.service';
import { ErrorHandler, Inject } from '@angular/core';

export class AppErrorHandler implements ErrorHandler {

    constructor(@Inject(ToasterService) private toastService: ToasterService) {

    }

    handleError(error: any): void {
        // console.log(error);
        this.toastService.display(error, 'error');
    }

}
