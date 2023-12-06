import { combineReducers, createStore, applyMiddleware } from 'redux'
import thunk from 'redux-thunk'
import blogReducer from 'Redux/BlogReducer'

export const appsProviderStore = createStore(combineReducers({
    blogReducer: blogReducer}),
    applyMiddleware(thunk));
