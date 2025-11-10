import { Component , Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import { WaybillDetails } from '../waybill-details';

@Component({
  selector: 'app-waybill-info',
  imports: [CommonModule],
  template: `
    <div *ngIf="waybill" class="result-card">
  <p><span>Waybill Number:</span> {{ waybill.waybillNumber }}</p>
  <p><span>Service Type:</span> {{ waybill.serviceType }}</p>
  <p><span>Sender:</span> {{ waybill.senderSuburb }}</p>
  <p><span>Recipient:</span> {{ waybill.recipientSuburb }}</p>

 <div *ngIf="waybill.parcelInfo?.length">
  <h4>Parcels:</h4>
  <table class="parcel-table">
    <thead>
      <tr>
        <th>Parcel Number</th>
        <th>Length (cm)</th>
        <th>Breadth (cm)</th>
        <th>Height (cm)</th>
        <th>Mass (kg)</th>
      </tr>
    </thead>
    <tbody>
      <tr *ngFor="let parcel of waybill.parcelInfo">
        <td>{{ parcel.parcelNumber }}</td>
        <td>{{ parcel.length }}</td>
        <td>{{ parcel.breadth }}</td>
        <td>{{ parcel.height }}</td>
        <td>{{ parcel.mass }}</td>
      </tr>
    </tbody>
  </table>
</div>

</div>

  `,
  styleUrls: ['./waybill-info.css']
})
export class WaybillInfoComponent {
 @Input() waybill!: WaybillDetails;
}
