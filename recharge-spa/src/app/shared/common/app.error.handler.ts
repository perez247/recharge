import { ToasterService } from './../_services/toaster.service';
import { ErrorHandler, Inject } from '@angular/core';

export class AppErrorHandler implements ErrorHandler {

    constructor(@Inject(ToasterService) private toastService: ToasterService) {

    }

    handleError(error: string): void {
        // console.log(error);
        const result = error.replace(new RegExp('%n%', 'g'), '\n');
        this.toastService.display(result, 'error');
    }

}
