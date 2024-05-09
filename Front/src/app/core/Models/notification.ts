export interface Notification{
id: string,
message: string,
createdAt: any,
}

export interface ResponseDto{
  isSuccess: boolean,
  result: Notification[],
  message: string
}
