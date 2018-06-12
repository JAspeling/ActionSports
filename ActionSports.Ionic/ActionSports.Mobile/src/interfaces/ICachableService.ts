export interface ICachableService {
    cachedItems: any[];
    addToCache(title: string, items: any[]);
}

export class ICachedItem {
    constructor(init? : Partial<ICachedItem>) {
        Object.assign(this, init);
    }

    title: string;
    cachedItems: any | any[];
}