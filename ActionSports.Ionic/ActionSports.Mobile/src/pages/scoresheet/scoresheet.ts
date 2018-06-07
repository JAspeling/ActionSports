import { LoadingController, NavParams, NavController, ToastController } from 'ionic-angular';
import { BasePage } from './../base-page';
import { Component } from '@angular/core';
import { MatchModel } from '../../classes/MatchModel';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
    selector: 'page-scoresheet',
    templateUrl: 'scoresheet.html'
})
export class ScoresheetPage extends BasePage {
    scoresheetUrl: SafeUrl;
    match: MatchModel;

    constructor(
        public loadingController: LoadingController,
        public navParams: NavParams,
        public navController: NavController,
        public sanitizer: DomSanitizer,
        public toastCtrl: ToastController
    ) {
        super(loadingController, navParams, navController, toastCtrl);
        this.createLoading('Fetching scoresheet...');
        this.match = this.navParams.data.match;
        this.scoresheetUrl = this.sanitizer.bypassSecurityTrustResourceUrl('http://actionsport.spawtz.com' + this.match.scoreHref);
    }
}
