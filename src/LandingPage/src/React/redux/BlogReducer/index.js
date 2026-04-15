import * as actionTypes from './actionTypes';

// src/js/reducers/index.js
const manageBlogState = {
    recentPosts: []
};

export function addRecentBlogPostsToState(recentPosts){
    return{
        type: actionTypes.GETRECENTBLOGPOSTS,
        payload: recentPosts
    };
}

export default function(state = manageBlogState, action) {
 // alert(JSON.stringify(action));

  switch (action.type) {
    case actionTypes.GETRECENTBLOGPOSTS:
        return Object.assign({}, state, {
            recentPosts: action.payload
        })
    default:
      return state;
  }
}

