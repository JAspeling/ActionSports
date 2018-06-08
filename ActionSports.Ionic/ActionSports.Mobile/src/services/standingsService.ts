import { StandingModel } from './../classes/StandingModel';
import { Injectable } from "@angular/core";
import { LeagueModel } from "../classes/LeagueModel";
import { HttpClient } from '@angular/common/http';

@Injectable() 
export class StandingsService {
    constructor(private httpClient: HttpClient) {

    }
    getStandings(league: LeagueModel): Promise<StandingModel[]> {
        return this.httpClient.post<StandingModel[]>(`http://jaspeling.grifdev.co.za/api/standings`, league).toPromise();
    } 
}