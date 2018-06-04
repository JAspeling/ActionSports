import { Component } from '@angular/core';
import { LeagueModel } from './../../classes/LeagueModel';
import { LeaguesService } from './../../services/leaguesService';
import { Loading, LoadingController, NavParams } from 'ionic-angular';
import { VenueModel } from '../../classes/VenueModel';

@Component({
    selector: 'page-league',
    templateUrl: 'leagues.html'
})
export class LeaguePage {
    venue: VenueModel;
    loader: Loading;
    leagues: LeagueModel[] = [];

    constructor(
        private navParams: NavParams,
        public loadingCtrl: LoadingController,
        public leagueService: LeaguesService
    ) {
        this.venue = this.navParams.data.venue;
    }

    ionViewDidLoad(): void {
        this.createLoading();
        this.leagues = [];
        this.loader.present().then(() => {
            this.leagueService
                .getLeagues(this.venue)
                .then((leagues: LeagueModel[]) => {
                    leagues.forEach(league => {
                        if (league.title.length > 55)
                            league.title = "..." + league.title.substring(league.title.length - 55,league.title.length)
                        this.leagues.push(league);
                    });
                    this.loader.dismiss();
                });
        });
    }

    createLoading() {
        this.loader = this.loadingCtrl.create({
            content: `Retrieving Leagues for ${this.venue.title}...`,
            spinner: 'dots'
        });
    }
}
