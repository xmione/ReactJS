import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface PeopleState {
    isLoading: boolean;
    startDateIndex?: number;
    forecasts: People[];
}

export interface People {
    id: number;
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestPeopleAction {
    type: 'REQUEST_PEOPLE';
    startDateIndex: number;
}

interface ReceivePeopleAction {
    type: 'RECEIVE_PEOPLE';
    startDateIndex: number;
    forecasts: People[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestPeopleAction | ReceivePeopleAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestPeople: (startDateIndex: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.people && startDateIndex !== appState.people.startDateIndex) {
            fetch(`people`)
            fetch(`https://localhost:7000/people`)
                .then(response => {
                    console.log(response);
                    return response.json() as Promise<People[]>;
                })
                //.then(response => response.json() as Promise<People[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_PEOPLE', startDateIndex: startDateIndex, forecasts: data });
                });

            dispatch({ type: 'REQUEST_PEOPLE', startDateIndex: startDateIndex });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: PeopleState = { forecasts: [], isLoading: false };

export const reducer: Reducer<PeopleState> = (state: PeopleState | undefined, incomingAction: Action): PeopleState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_PEOPLE':
            return {
                startDateIndex: action.startDateIndex,
                forecasts: state.forecasts,
                isLoading: true
            };
        case 'RECEIVE_PEOPLE':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.startDateIndex === state.startDateIndex) {
                return {
                    startDateIndex: action.startDateIndex,
                    forecasts: action.forecasts,
                    isLoading: false
                };
            }
            break;
    }

    return state;
};
