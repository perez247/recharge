import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BankService {

  constructor() { }

  private banks: any[] = [
      { 'id': '1', 'name': 'Access Bank' , 'code': '044', 'value': 'accessBank' },
      { 'id': '2', 'name': 'Citibank', 'code': '023', 'value': 'citiBank' },
      { 'id': '3', 'name': 'Diamond Bank', 'code': '063', 'value': 'diamondBank' },
      { 'id': '4', 'name': 'Dynamic Standard Bank', 'code': '', 'value': 'dynamicBank'},
      { 'id': '5', 'name': 'Ecobank Nigeria', 'code': '050', 'value': 'ecoBank' },
      { 'id': '6', 'name': 'Fidelity Bank Nigeria', 'code': '070', 'value': 'fidelityBank' },
      { 'id': '7', 'name': 'First Bank of Nigeria', 'code': '011', 'value': 'firstBank' },
      { 'id': '8', 'name': 'First City Monument Bank', 'code': '214', 'value': 'firstcityBank' },
      { 'id': '9', 'name': 'Guaranty Trust Bank', 'code': '058', 'value': 'guarantyTrustBank' },
      { 'id': '10', 'name': 'Heritage Bank Plc', 'code': '030', 'value': 'heritageBank' },
      { 'id': '11', 'name': 'Jaiz Bank', 'code': '301', 'value': 'jaiztBank' },
      { 'id': '12', 'name': 'Keystone Bank Limited', 'code': '082', 'value': 'keystoneBank' },
      { 'id': '13', 'name': 'Providus Bank Plc', 'code': '101', 'value': 'providusBank' },
      { 'id': '14', 'name': 'Skye Bank', 'code': '076', 'value': 'skyBank' },
      { 'id': '15', 'name': 'Stanbic IBTC Bank Nigeria Limited', 'code': '221', 'value': 'stanbicBank' },
      { 'id': '16', 'name': 'Standard Chartered Bank', 'code': '068', 'value': 'standardCharteredBank' },
      { 'id': '17', 'name': 'Sterling Bank', 'code': '232', 'value': 'steringBank' },
      { 'id': '18', 'name': 'Suntrust Bank Nigeria Limited', 'code': '100', 'value': 'suntrustBank' },
      { 'id': '19', 'name': 'Union Bank of Nigeria', 'code': '032', 'value': 'unionBank' },
      { 'id': '20', 'name': 'United Bank for Africa', 'code': '033', 'value': 'unitedBank' },
      { 'id': '21', 'name': 'Unity Bank Plc', 'code': '215', 'value': 'unityBank' },
      { 'id': '22', 'name': 'Wema Bank', 'code': '035', 'value': 'wemaBank' },
      { 'id': '23', 'name': 'Zenith Bank', 'code': '057', 'value': 'zenithBank' }
    ];

  getBanks() {
    return of(this.banks);
  }
}
