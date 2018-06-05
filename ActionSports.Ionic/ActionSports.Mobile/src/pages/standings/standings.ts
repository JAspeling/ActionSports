import { BasePage } from './../base-page';
import { Component } from '@angular/core';
import { LeagueModel } from './../../classes/LeagueModel';
import { LoadingController, NavParams } from 'ionic-angular';
import { StandingModel } from '../../classes/StandingModel';
import { StandingsService } from '../../services/standingsService';

@Component({
    selector: 'page-standings',
    templateUrl: 'standings.html'
})
export class StandingsPage extends BasePage {
    league: LeagueModel;
    standings: StandingModel[] = [];

    constructor(
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        public standingsService: StandingsService
    ) {
        super(loadingCtrl, navParams, undefined);
        this.createLoading('Retrieving Standings...');

        this.league = this.navParams.data.league;
    }

    ionViewDidLoad(): void {
        this.standings = [];
        this.loader.present().then(() => {
            this.standingsService
                .getStandings(this.league)
                .then((standings: StandingModel[]) => {
                    if (standings) {
                        standings.forEach(standing => {
                            this.standings.push(standing);
                        });
                    }
                    this.loader.dismiss();
                });
        });
    }

    expandItem(standing) {
        this.standings.map(listItem => {
            if (standing == listItem) {
                listItem.isExpanded = !listItem.isExpanded;
            } else {
                listItem.isExpanded = false;
            }
            return listItem;
        });
    }

    showScoresheet(event : Event) {
        event.stopPropagation();
    }
}
