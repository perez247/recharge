import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {

  settings = {
    action : 'close',
    config: {
      duration : 20000,
      panelClass: ['bg-light']
    }
  };

  toastSetting = {
    duration: 2000,
    showCloseButton: true,
    position : 'top'
  };

  constructor(private toast: MatSnackBar, private toaster: ToastController) {
    // alertify.defaults.notifier.delay = 10;
  }

  private addCss(className: string) {
    this.settings.config.panelClass.length = 0;
    this.settings.config.panelClass = ['text-center', className];
  }

  private async makeToast(message: string, type: string) {

    const toast = await this.toaster.create({
      message,
      color: type,
      duration: 3000,
      showCloseButton: true,
      position : 'top'
    });

    toast.present();
  }

  // confirm(message: string, okCallBack: () => any) {
  //   console.log(message);
  // }

  async success(message: string) {
    // this.addCss('text-success');
    // this.toast.open(message, this.settings.action, {...this.settings.config});
    await this.makeToast(message, 'success');
  }

  async error(message: string) {
    // this.addCss('text-danger');
    // this.toast.open(message, this.settings.action, {...this.settings.config});
    await this.makeToast(message, 'danger');
  }

  async warning(message: string) {
    // this.addCss('text-warning');
    // this.snackBar.open(message, this.settings.action, {...this.settings.config});
    await this.makeToast(message, 'warning');

  }

  async message(message: string) {
    // console.log(message);
    // this.addCss('text-dark');
    // this.snackBar.open(message, this.settings.action, {...this.settings.config});
    await this.makeToast(message, 'secondary');
  }



}
