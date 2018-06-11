import { AppState } from './../classes/AppState';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LeagueModel } from '../classes/LeagueModel';
import { VenueModel } from '../classes/VenueModel';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/timeout';

@Injectable()
export class LeaguesService {
    constructor(private httpClient: HttpClient) {}
    // http://localhost:5000/api/league
    public getLeagues(venue: VenueModel): Promise<LeagueModel[]> {
        return this.httpClient.post<LeagueModel[]>(`${AppState.basePage}/league`, venue).timeout(15000).toPromise();
    }
}
