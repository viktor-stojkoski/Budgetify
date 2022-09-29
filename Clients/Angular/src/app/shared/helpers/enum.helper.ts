import { ITranslationEnum } from '../models/shared.model';

export function enumToTranslationEnum(enumType: { [x: string]: string }): ITranslationEnum[] {
  return Object.entries(enumType).map(([value, translationKey]) => ({ translationKey, value }));
}
