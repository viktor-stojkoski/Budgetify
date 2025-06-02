import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
} from "@/components/ui/navigation-menu";
import { TranslationKeys } from "../_static/translationKeys";
import { useTranslations } from "next-intl";
import NavbarAuth from "./navbar-auth";
import LocaleSwitcher from "./locale-switcher";

const Navbar = () => {
  const t = useTranslations();
  const translationKeys = TranslationKeys;
  const isAuthenticated = true;
  const menuItems = [
    {
      href: "/categories",
      title: t(translationKeys.navbarCategoriesMenu),
    },
    {
      href: "/exchange-rates",
      title: t(translationKeys.navbarExchangeRatesMenu),
    },
    {
      href: "/accounts",
      title: t(translationKeys.navbarAccountsMenu),
    },
    {
      href: "/merchants",
      title: t(translationKeys.navbarMerchantsMenu),
    },
    {
      href: "/transactions",
      title: t(translationKeys.navbarTransactionsMenu),
    },
    {
      href: "/budgets",
      title: t(translationKeys.navbarBudgetsMenu),
    },
  ];

  return (
    <div className="border-b">
      <div className="flex h-16 items-center container mx-auto">
        <NavigationMenu className="mr-auto">
          {isAuthenticated && (
            <NavigationMenuList>
              {menuItems.map((menuItem) => (
                <NavigationMenuItem key={menuItem.href}>
                  <NavigationMenuLink
                    href={menuItem.href}
                    className="flex w-full items-center py-2 text-lg font-semibold"
                  >
                    {menuItem.title}
                  </NavigationMenuLink>
                </NavigationMenuItem>
              ))}
            </NavigationMenuList>
          )}
        </NavigationMenu>
        <NavbarAuth isAuthenticated={isAuthenticated} />
        <LocaleSwitcher />
      </div>
    </div>
  );
};

export default Navbar;
