import { VenueModel } from './../classes/VenueModel';
import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/timeout';

@Injectable()
export class VenuesService {
    constructor(private httpClient: HttpClient) {

    }

    // http://localhost:5000/api/venues
    public getVenues() : Promise<VenueModel[]> {
        return this.httpClient.get<VenueModel[]>(`http://action.jaspeling.co.za/ActionSports.API/api/venues`).timeout(15000).toPromise();
    }
}