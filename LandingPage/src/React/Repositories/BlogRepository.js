import { RestClient } from './RestClient'

export class BlogRepository extends RestClient {
     getMostRecentPosts(numberToGet, responseHandler) {
        let getUrl = 'http://blog.alwaysmoveforward.com/api/BlogPosts/' + numberToGet;
        this.getRequest(getUrl, responseHandler);
    }
}