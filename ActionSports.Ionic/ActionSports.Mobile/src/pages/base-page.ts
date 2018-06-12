import {
    LoadingController,
    Loading,
    NavParams,
    NavController,
    ToastController
} from 'ionic-angular';

export class BasePage {
    activeMenu: string;
    loader: Loading;
    navOptions: {
        animation: string;
    };

    constructor(
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        public navCtrl: NavController,
        public toastCtrl: ToastController
    ) {
        if (this.navOptions) this.navOptions.animation = 'wd-transition';
    }

    createLoading(message: string) {
        this.loader = this.loadingCtrl.create({
            content: message,
            dismissOnPageChange: true
        });
    }

    presentToast(message: string) {
        console.log(message);
        let toast = this.toastCtrl.create({
          message: message,
          showCloseButton: true,
          closeButtonText: 'Ok',
          dismissOnPageChange: true
        });
        toast.present();
    }
}
