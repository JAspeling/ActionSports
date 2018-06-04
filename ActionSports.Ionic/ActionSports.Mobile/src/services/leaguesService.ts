import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { LeagueModel } from "../classes/LeagueModel";
import { VenueModel } from "../classes";

@Injectable()
export class LeaguesService {
    constructor(private httpClient: HttpClient) {

    }

    public getLeagues(venue: VenueModel) : Promise<LeagueModel[]> {
        return this.httpClient.post<LeagueModel[]>("http://localhost:5000/api/league", venue).toPromise();
    }
}