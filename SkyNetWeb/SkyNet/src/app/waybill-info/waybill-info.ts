import { Component , Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import { WaybillDetails } from '../waybill-details';
import { ParcelInfo } from '../parcel-info';
@Component({
  selector: 'app-waybill-info',
  imports: [CommonModule],
  template: `
     <div *ngIf="waybill" class="result-card">
      <p><span>Waybill Number:</span> {{ waybill.waybillNumber }}</p>
      <p><span>Service Type:</span> {{ waybill.serviceType }}</p>
      <p><span>Sender:</span> {{ waybill.senderSuburb }}</p>
      <p><span>Recipient:</span> {{ waybill.recipientSuburb }}</p>
      <p><span>Parcel Number:</span> {{ waybill.parcelInfo.parcelNumber }}</p>
    </div>
  `,
  styles: ``
})
export class WaybillInfoComponent {
 @Input() waybill!: WaybillDetails;
}
