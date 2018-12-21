import { Injectable } from '@angular/core';
import { ToastController, AlertController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {

  config: any = {
    duration: 10000,
    position: 'top'
  };

  constructor(private toaster: ToastController, private alertCtrl: AlertController) { }

  async display(message: string, type = 'normal') {
    const toast = await this.toaster.create({
      message,
      cssClass: `${type}-toast`,
      ...this.config,
      background: '#fff'
    });

    toast.present();
  }

  async shout(header = 'Info', message: string, action: () => any) {
    const altCtrl = await this.alertCtrl.create({
      header,
      message: message,
      buttons: [{
        text: 'Ok',
        handler: action
      }]
    });

    await altCtrl.present();
  }

}
