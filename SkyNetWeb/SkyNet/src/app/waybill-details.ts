import { ParcelInfo } from "./parcel-info";

export interface WaybillDetails {
    id: string; 
    waybillNumber: string;
    serviceType: string;

    senderSuburb: string;
    senderPostalCode: string;

    recipientSuburb: string;
    recipientPostalCode: string;

    parcelInfo: ParcelInfo[];
}
