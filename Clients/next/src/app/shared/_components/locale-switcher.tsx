import { useLocale, useTranslations } from "next-intl";
import LocaleSwitcherSelect from "./locale-switcher-select";
import { TranslationKeys } from "../_static/translationKeys";

export interface ILanguage {
  name: string;
  resource: string;
}

const LocaleSwitcher = () => {
  const t = useTranslations();
  const translationKeys = TranslationKeys;
  const locale = useLocale();
  const languages: ILanguage[] = [
    {
      name: translationKeys.navbarLanguageEnglish,
      resource: "en",
    },
    {
      name: translationKeys.navbarLanguageMacedonian,
      resource: "mk",
    },
  ];

  return (
    <LocaleSwitcherSelect defaultValue={locale} label="test">
      {languages.map((current) => (
        <option key={current.resource} value={current.resource}>
          {t(current.name)}
        </option>
      ))}
    </LocaleSwitcherSelect>
  );
};

export default LocaleSwitcher;
