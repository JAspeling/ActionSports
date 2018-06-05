import { BasePage } from './../base-page';
import { Component } from '@angular/core';
import { LeagueModel } from './../../classes/LeagueModel';
import { LoadingController, NavParams, NavController } from 'ionic-angular';
import { StandingModel } from '../../classes/StandingModel';
import { StandingsService } from '../../services/standingsService';
import { ScoresheetPage } from '../scoresheet/scoresheet';
import { MatchModel } from '../../classes/MatchModel';

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
        public standingsService: StandingsService,
        public navCtrl : NavController
    ) {
        super(loadingCtrl, navParams, navCtrl);
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

    showScoresheet(event : Event, match: MatchModel) {
        debugger;
        event.stopPropagation();
        
        this.navCtrl.push(ScoresheetPage, {match: match}, this.navOptions);
    }
}
