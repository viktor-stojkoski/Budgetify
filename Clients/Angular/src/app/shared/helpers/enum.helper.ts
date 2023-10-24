import { ITranslationEnum } from '../models/shared.model';

export function enumToTranslationEnum(enumType: { [x: string]: string }): ITranslationEnum[] {
  return Object.entries(enumType).map(([value, translationKey]) => ({ translationKey, value }));
}

export function getEnumKeyFromValue(enumType: { [x: string]: string }, value: string): string {
  return Object.keys(enumType)[Object.values(enumType).indexOf(value)];
}
