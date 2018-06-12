import 'rxjs/add/operator/timeout';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { AppState } from '../classes/AppState';
import { LeagueModel } from '../classes/LeagueModel';
import { StandingModel } from './../classes/StandingModel';
import { ICachedItem, ICachableService } from '../interfaces/ICachableService';

@Injectable() 
export class StandingsService implements ICachableService {

    cachedItems: CachedStanding[] = [];

    constructor(private httpClient: HttpClient) {

    }

    addToCache(title: string, items: StandingModel[]): any {
        if (!this.cachedItems.find(cachedItem => cachedItem.title == title)) {
            this.cachedItems.push({title: title, cachedItems: items.slice()})
        }
    }

    // http://localhost:5000/api/standings
    getStandings(league: LeagueModel): Promise<StandingModel[]> {
        const cachedStanding = this.cachedItems.find(cacheItem => cacheItem.title == league.title)
        if (cachedStanding) {
            return new Promise((resolve, reject) => {resolve(cachedStanding.cachedItems)});
        } else
        return this.httpClient.post<StandingModel[]>(`${AppState.basePage}/standings`, league).timeout(15000).toPromise();
    } 
}

export class CachedStanding implements ICachedItem {
    title: string;
    cachedItems: StandingModel[];
    constructor(init? : Partial<CachedStanding>) {
        Object.assign(this, init);
    }
}
