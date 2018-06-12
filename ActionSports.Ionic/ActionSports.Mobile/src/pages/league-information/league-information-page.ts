import { FixturePage } from './../fixtures/fixture-page';
import { Component, ViewChild } from '@angular/core';
import { LeagueModel } from '../../classes/LeagueModel';
import { BasePage } from '../base-page';
import { LoadingController, NavParams, NavController, Tabs, ToastController } from 'ionic-angular';
import { StandingsService } from '../../services/standingsService';
import { StandingsPage } from '../standings/standings';

@Component({
    selector: 'league-information-page',
    templateUrl: './league-information-page.html'
})
export class LeagueInformationPage extends BasePage {
    league: LeagueModel;
    @ViewChild('tabs') tabRef: Tabs;
    standingsPage = StandingsPage;
    fixturePage = FixturePage;

    constructor(
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        public standingsService: StandingsService,
        public navCtrl: NavController,
        public toastCtrl: ToastController
    ) {
        super(loadingCtrl, navParams, navCtrl, toastCtrl);
        this.createLoading('Retrieving Standings...');
        this.league = this.navParams.data.league;
        console.log('League', this.league);
    }
}
