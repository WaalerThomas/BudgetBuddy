export class Response<T> {
    data: T;
    status: Status;
}

export class Status {
    code: StatusCodeEnum;
    message?: string;
}

export const enum StatusCodeEnum {
    Ok = 1,
    Warning = 2,
    Error = 3,
}