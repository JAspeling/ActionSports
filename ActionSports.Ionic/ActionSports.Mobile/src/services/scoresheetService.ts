import { ICachableService, ICachedItem } from './../interfaces/ICachableService';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { File } from '@ionic-native/file';
import { SocialSharing } from '@ionic-native/social-sharing';

import { AppState } from '../classes/AppState';
import { MatchModel } from './../classes/MatchModel';

@Injectable()
export class ScoresheetService implements ICachableService {

    cachedItems: CachableScoresheet[] = [];
    addToCache(title: string, base64: any) {
        if (!this.cachedItems.find(cachedItem => cachedItem.title == title)) {
            this.cachedItems.push({title: title, cachedItems: base64});
        }
    }

    constructor(
        private httpClient: HttpClient,
        private file: File,
        private socialSharing: SocialSharing
    ) {}

    convertToPdf(match: MatchModel): Promise<string> {
        const clone = new MatchModel(match);
        clone.scoreHref = AppState.baseUrl + match.scoreHref;
        const cachedItem = this.cachedItems.find(cacheItem => cacheItem.title == match.score)
        if (cachedItem) {
            return new Promise((resolve, reject) => {resolve(cachedItem.cachedItems)});
        } else
        return this.httpClient
            .post<string>(`${AppState.basePage}/scoresheet`, clone)
            .timeout(30000)
            .toPromise();
    }

    public saveBase64(base64: string, name: string, match: MatchModel): Promise<MessageObject> {
        return new Promise((resolve, reject) => {
            var realData = base64.split(',')[1];
            let blob = this.b64toBlob(base64, 'application/pdf', 512);
            
            let imageName = `${name}.pdf`;
            const ROOT_DIRECTORY = 'file:///sdcard//';
            const downloadFolderName = 'actionSportsTemp';
            const fileDir = ROOT_DIRECTORY + downloadFolderName + '/' + imageName;

            this.file
            .checkFile(ROOT_DIRECTORY + downloadFolderName, imageName).then((exists) => {
                resolve(new MessageObject({message: `${match.teamA} vs ${match.teamB} - ${match.score}`, fileUrl: fileDir}))
            }, err => {
                this.file.writeFile(ROOT_DIRECTORY + downloadFolderName, imageName, blob)
                .then(() => {
                    resolve(new MessageObject({message: `${match.teamA} vs ${match.teamB} - ${match.score}`, fileUrl: fileDir}))
                }).catch(err => {
                    console.log('error writing blob');
                    reject(err);
                });
            })                
        });
    }

    b64toBlob(b64Data, contentType, sliceSize) {
        contentType = contentType || '';
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(b64Data);
        var byteArrays = [];

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
          var slice = byteCharacters.slice(offset, offset + sliceSize);

          var byteNumbers = new Array(slice.length);
          for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
          }

          var byteArray = new Uint8Array(byteNumbers);

          byteArrays.push(byteArray);
        }

        var blob = new Blob(byteArrays, {type: contentType});
        return blob;
      }
}

export class CachableScoresheet implements ICachedItem {
    title: string;
    cachedItems: any | any[];
} 

export class MessageObject {
    constructor(init? : Partial<MessageObject>) {
        Object.assign(this, init);
    }
    message: string;
    title: string;
    fileUrl: string;
}