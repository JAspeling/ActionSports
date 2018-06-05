import { VenueModel } from './../classes/VenueModel';
import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class VenuesService {
    constructor(private httpClient: HttpClient) {

    }

    public getVenues() : Promise<VenueModel[]> {
        return this.httpClient.get<VenueModel[]>(`http://localhost:5000/api/venues`).toPromise();
    }
}