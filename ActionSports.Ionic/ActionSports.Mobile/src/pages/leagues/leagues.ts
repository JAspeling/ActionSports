import { LeagueInformationPage } from './../league-information/league-information-page';
import { BasePage } from './../base-page';
import { Component, NgZone } from '@angular/core';
import {
    Item,
    ItemSliding,
    LoadingController,
    NavController,
    NavParams,
    ToastController
} from 'ionic-angular';
import { LeagueModel } from './../../classes/LeagueModel';
import { LeaguesService } from './../../services/leaguesService';
import { VenueModel } from '../../classes/VenueModel';

@Component({
    selector: 'page-league',
    templateUrl: 'leagues.html'
})
export class LeaguePage extends BasePage {
    venue: VenueModel;
    leagues: LeagueModel[] = [];
    isOpen: boolean = false;

    constructor(
        public navParams: NavParams,
        public navCtrl: NavController,
        public loadingCtrl: LoadingController,
        public leagueService: LeaguesService,
        public toastCtrl: ToastController,
        private zone: NgZone
    ) {
        super(loadingCtrl, navParams, navCtrl, toastCtrl);
        this.venue = this.navParams.data.venue;
    }

    ionViewDidEnter(){
        this.leagues = [];
        this.createLoading(`Retrieving Leagues for ${this.venue.title}...`);
        this.loader.present().then(() => {
            this.leagueService.getLeagues(this.venue).then(
                (leagues: LeagueModel[]) => {
                    // add the retrieved values to the cache if it hasnt been cached yet.
                    this.leagueService.addToCache(this.venue, leagues);
                    console.log('Data retrieved.');
                    this.logDate();
                    leagues.forEach(league => {
                        this.leagues.push(league);
                    });
                    console.log('Dismissing loader');
                    this.logDate();
                    this.loader.dismiss();
                },
                err => {
                    console.error('Failed to retrieve leagues', err);
                    this.loader.dismiss();
                    this.presentToast('Failed to retrieve leagues');
                }
            );
        });
    }

    navigateToStandings(league: LeagueModel) {
        this.navCtrl.push(
            LeagueInformationPage,
            { league: league },
            this.navOptions
        );
    }

    logDate() {
        const d = new Date();
        console.log(`Time: ${d.getTime()}`);
    }

    toggleState(slidingItem: ItemSliding, item: Item) {
        // if (this.isOpen) {
        //     this.close(slidingItem)
        // } else {
        //     this.open(slidingItem, item);
        // }
    }

    // open(itemSlide: ItemSliding, item: Item) {

    //     // reproduce the slide on the click
    //     itemSlide.setElementClass("active-sliding", true);
    //     itemSlide.setElementClass("active-slide", true);
    //     itemSlide.setElementClass("active-options-right", true);
    //     item.setElementStyle("transform", "translate3d(-144px, 0px, 0px)")

    // }

    // close(slidingItem: ItemSliding) {
    //     slidingItem.close();
    //     slidingItem.setElementClass("active-slide", false);
    //     slidingItem.setElementClass("active-slide", false);
    //     slidingItem.setElementClass("active-options-right", false);
    // }
}
