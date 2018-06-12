import 'rxjs/add/operator/timeout';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { AppState } from '../classes/AppState';
import { LeagueModel } from '../classes/LeagueModel';
import { StandingModel } from './../classes/StandingModel';

@Injectable() 
export class StandingsService {
    constructor(private httpClient: HttpClient) {

    }

    // http://localhost:5000/api/standings
    getStandings(league: LeagueModel): Promise<StandingModel[]> {
        return this.httpClient.post<StandingModel[]>(`${AppState.basePage}/standings`, league).timeout(15000).toPromise();
    } 
}