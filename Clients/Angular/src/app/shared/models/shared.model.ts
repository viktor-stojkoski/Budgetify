import { DialogActionButton } from './shared.enum';

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

export interface DialogResponseData {
  action: DialogActionButton;
}
