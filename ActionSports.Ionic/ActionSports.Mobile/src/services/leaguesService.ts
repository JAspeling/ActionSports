import 'rxjs/add/operator/timeout';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { LeagueModel } from '../classes/LeagueModel';
import { VenueModel } from '../classes/VenueModel';
import { AppState } from './../classes/AppState';

@Injectable()
export class LeaguesService {
    constructor(private httpClient: HttpClient) {}
    // http://localhost:5000/api/league
    public getLeagues(venue: VenueModel): Promise<LeagueModel[]> {
        return this.httpClient.post<LeagueModel[]>(`${AppState.basePage}/league`, venue).timeout(15000).toPromise();
    }
}
