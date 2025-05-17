"use client";

import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuList,
} from "@/components/ui/navigation-menu";
import { useTranslations } from "next-intl";
import { TranslationKeys } from "../_static/translationKeys";

const NavbarAuth = ({ isAuthenticated }: { isAuthenticated: boolean }) => {
  const t = useTranslations();
  const translationKeys = TranslationKeys;
  const currentUser = {
    firstName: "Viktor",
    lastName: "Stojkoski",
  };

  const login = (): void => {
    console.log("login");
  };

  const logout = (): void => {
    console.log("logout");
  };

  const editProfile = (): void => {
    console.log("Edit profile");
  };

  return (
    <NavigationMenu>
      {!isAuthenticated ? (
        <NavigationMenuList>
          <NavigationMenuItem className="flex w-full items-center py-2 text-lg font-semibold">
            <button onClick={login}>
              {t(translationKeys.navbarLoginOrRegister)}
            </button>
          </NavigationMenuItem>
        </NavigationMenuList>
      ) : (
        <NavigationMenuList className="w-full">
          <NavigationMenuItem className="flex w-full items-center py-2 text-lg font-semibold">
            <button onClick={editProfile}>
              {`${currentUser.firstName} ${currentUser.lastName}`}
            </button>
          </NavigationMenuItem>
          <NavigationMenuItem className="flex w-30 items-center py-2 text-lg font-semibold">
            <button onClick={logout}>{t(translationKeys.navbarLogout)}</button>
          </NavigationMenuItem>
        </NavigationMenuList>
      )}
    </NavigationMenu>
  );
};

export default NavbarAuth;
