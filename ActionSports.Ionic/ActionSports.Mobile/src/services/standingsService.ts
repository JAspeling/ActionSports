import { BasePage } from './../pages/base-page';
import { StandingModel } from './../classes/StandingModel';
import { Injectable } from "@angular/core";
import { LeagueModel } from "../classes/LeagueModel";
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/timeout';
import { AppState } from '../classes/AppState';

@Injectable() 
export class StandingsService {
    constructor(private httpClient: HttpClient) {

    }

    // http://localhost:5000/api/standings
    getStandings(league: LeagueModel): Promise<StandingModel[]> {
        return this.httpClient.post<StandingModel[]>(`${AppState.basePage}/standings`, league).timeout(15000).toPromise();
    } 
}