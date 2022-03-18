export interface IGlobalStore {
    market: string,
    limit: string,
    genres: Array<string>,
    artists: {
        items: [
            {
                name: string,
                id: string,
                images: [
                    {
                        href: string,
                    },
                ],
            },
        ],
    },
    tracks: {
        items: [
            {
                name: string,
                id: string,
                images: [
                    {
                        href: string,
                    },
                ],
            },
        ],
    }
}