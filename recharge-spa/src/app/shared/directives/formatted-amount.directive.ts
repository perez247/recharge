import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appFormattedAmount]'
})
export class FormattedAmountDirective {

  constructor(private el: ElementRef) { }

  @HostListener('blur') onChange() {
    // $event.target.value = $event.target.value ? parseFloat($event.target.value).toFixed(2) : 0.00;
    const value = this.el.nativeElement.value;
    this.el.nativeElement.value = value ? parseFloat(value).toFixed(2) : '0.00';
    // console.log('changed');
  }

}
