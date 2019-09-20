
export class SignUpModel {
    private username: string;
    private countryCode: string;
    private phoneNumber: string;
    private pin: string;
    private referersCountryCode: string;
    private referersPhoneNumber: string;

    constructor(data: Partial<SignUpModel>) {
        Object.assign(this, data);
    }

    createFormData() {
        const phone = `${this.countryCode}-${this.phoneNumber}`;

        let refNumber = null;
        if (this.referersCountryCode && this.referersPhoneNumber) {
            refNumber = `${this.referersCountryCode}-${this.referersPhoneNumber}`;
        }

        return {
            username : this.username,
            phone,
            pin: this.pin,
            ReferersPhone: refNumber

        };
    }
}
