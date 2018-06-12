import { VenueModel } from './../classes/VenueModel';
import { LeagueModel } from './../classes/LeagueModel';
import 'rxjs/add/operator/timeout';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { AppState } from './../classes/AppState';

@Injectable()
export class LeaguesService {
    cachedLeagues: CachedLeague[] = [];
    
    constructor(private httpClient: HttpClient) {}

    addToCache(venue: VenueModel, leagues: LeagueModel[]): any {
        if (!this.cachedLeagues.find(l => l.venue == venue.title)) {
            this.cachedLeagues.push({venue: venue.title, leagueModels: leagues.slice()})
        }
    }

    // http://localhost:5000/api/league
    public getLeagues(venue: VenueModel): Promise<LeagueModel[]> {
        const cachedLeague = this.cachedLeagues.find(l => l.venue == venue.title)
        if (cachedLeague) {
            return new Promise((resolve, reject) => {resolve(cachedLeague.leagueModels)});
        } else
        return this.httpClient.post<LeagueModel[]>(`${AppState.basePage}/league`, venue).timeout(15000).toPromise();
    }
}

export class CachedLeague {
    constructor(init? : Partial<CachedLeague>) {
        Object.assign(this, init);
    }
    venue: string = '';
    leagueModels: LeagueModel[] = [];
}
