export interface IResult<T> {
  message?: string;
  isSuccess: boolean;
  isFailure: boolean;
  isEmpty: boolean;
  value?: T;
}

export interface ITranslationEnum {
  translationKey: string;
  value: string;
}
