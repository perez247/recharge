import { AppToken } from '../../app-domain/app-token';
import { tassign } from 'tassign';
import { TokenService } from '../../services/token/token.service';
import { UserActionConstant } from './user-action-constant';

export const AppInjector = {
    tokenService: {} as TokenService
};

export interface IUserAppState {
    token: Promise<AppToken | null>;
}

export const USER_INITIAL_STATE: IUserAppState = {
    token: Object.assign({}) as Promise<AppToken | null>
};

export class UserActions {

    // static dataSource = new BehaviorSubject<{}>({});
    // static data = UserActions.dataSource.asObservable();
    // static currentData = {} as Promise<AppToken | null>;

    tokenService: TokenService;

    constructor(private state: IUserAppState, private action: any) {
        this.tokenService = AppInjector.tokenService;
    }

    // static updateData(state: string, data: any) {
    //     UserActions.dataSource.next({state, data});
    // }

    saveUser(state: string) {

        // UserActions.updateData(state, this.action.payload);

        // console.log(UserActions.currentData);
        this.tokenService.save(this.action.payload);

        return tassign(this.state, { token: this.tokenService.tokenAsObject() });
    }

    setUser(state: string) {

        // UserActions.updateData(state, this.action.token);

        return tassign(this.state, { token: this.tokenService.tokenAsObject() });
    }

    logout(state: string) {
        // UserActions.updateData(state, this.action.token);

        this.tokenService.clear();

        return tassign(this.state, { token: null});
    }
}

export function userReducer(state: IUserAppState = USER_INITIAL_STATE, action): IUserAppState  {

    // return state;

    const obj = new UserActions(state, action);

    switch (action.type) {
        case UserActionConstant.SAVE_AUTH_USER: return obj.saveUser(UserActionConstant.SAVE_AUTH_USER);
        case UserActionConstant.SET_AUTH_USER: return obj.setUser(UserActionConstant.SET_AUTH_USER);
        case UserActionConstant.LOG_OUT: return obj.logout(UserActionConstant.LOG_OUT);
        default: return state;
    }

}
