import { Component, inject } from '@angular/core';
import { WaybillInfoComponent } from '../waybill-info/waybill-info';
import { WaybillDetails } from '../waybill-details';
import { ParcelInfo } from '../parcel-info';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WaybillService } from '../service/waybill';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, WaybillInfoComponent, FormsModule],
  template: `
    <div class="search-container">
        <h2>Search Waybill</h2>
        <input 
          type="text" 
          id="waybillNumber" 
          placeholder="Enter Waybill Number"
          [(ngModel)]="searchTerm"
          (keyup.enter)="searchWaybill()">
        <button (click)="searchWaybill()" [disabled]="isLoading">
          {{ isLoading ? 'Searching...' : 'Search' }}
        </button>

        <div *ngIf="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <section class="results" *ngIf="waybillFound">
          <app-waybill-info [waybill]="waybill"></app-waybill-info>
        </section>
    </div>
  `,
  styleUrls: ['./home.css']
})
export class HomeComponent {
  searchTerm: string = '';
  private apiUrl = 'http://localhost:5000/waybills';
  private http = inject(HttpClient);
  
  waybillFound: boolean = false; // Flag to track if waybill was successfully found
  isLoading: boolean = false;
  errorMessage: string = '';
  
  waybill: WaybillDetails = {
    id: crypto.randomUUID(),
    waybillNumber: '',
    serviceType: '',
    senderSuburb: '',
    senderPostalCode: '',
    recipientSuburb: '',
    recipientPostalCode: '',
    parcelInfo: {
      id: "",
      length: 0,
      breadth: 0,
      height: 0,
      mass: 0,
      parcelNumber: ''
    }
  };

  searchWaybill() {
    if (!this.searchTerm.trim()) {
      this.errorMessage = 'Please enter a waybill number';
      this.waybillFound = false;
      return;
    }

    console.log('Searching for waybill:', this.searchTerm);
    
    this.isLoading = true;
    this.errorMessage = '';
    this.waybillFound = false; // Hide previous results

    // Call the API
    this.getWaybillByNumber(this.searchTerm).subscribe({
      next: (result: WaybillDetails) => {
        // Update the waybill object with API response
        this.waybill = result;
        this.waybillFound = true; // Show the waybill component
        this.isLoading = false;
        console.log('Waybill found:', result);
      },
      error: (err) => {
        console.error('Waybill not found or error occurred:', err);
        this.errorMessage = err.status === 404 
          ? 'Waybill not found' 
          : 'An error occurred while searching. Please try again.';
        this.waybillFound = false; // Keep it hidden
        this.isLoading = false;
      }
    });
  }

  getWaybillByNumber(number: string): Observable<WaybillDetails> {
    return this.http.get<WaybillDetails>(`${this.apiUrl}/${number}`);
  }
}