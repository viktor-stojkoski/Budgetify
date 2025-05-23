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

export interface IDialogResponseData {
  action: DialogActionButton;
}

export interface IFileForUpload {
  name: string;
  type: string;
  size: number;
  content: number[];
}
