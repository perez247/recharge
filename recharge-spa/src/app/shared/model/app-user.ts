import { Datetime } from '@ionic/angular';

export interface AppUser {
    id: string;
    userName: string;
    phoneNumber: string;
    expires: Date;
    code: string;
}
