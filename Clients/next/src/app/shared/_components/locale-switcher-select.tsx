"use client";

import { setCookie } from "@/lib/utils";
import clsx from "clsx";
import { useRouter } from "next/navigation";
import { ChangeEvent, ReactNode } from "react";

const LocaleSwitcherSelect = ({
  children,
  defaultValue,
  label,
}: {
  children: ReactNode;
  defaultValue: string;
  label: string;
}) => {
  const router = useRouter();

  const onSelectChange = async (event: ChangeEvent<HTMLSelectElement>) => {
    const nextLocale = event.target.value;
    setCookie("NEXT_LOCALE", nextLocale);
    router.refresh();
  };

  return (
    <label
      className={clsx(
        "relative text-gray-400",
        "transition-opacity [&:disabled]:opacity-30"
      )}
    >
      <p className="sr-only">{label}</p>
      <select
        className="inline-flex appearance-none bg-transparent py-3 pl-2 pr-6"
        defaultValue={defaultValue}
        onChange={onSelectChange}
      >
        {children}
      </select>
      <span className="pointer-events-none absolute right-2 top-[8px]">⌄</span>
    </label>
  );
};

export default LocaleSwitcherSelect;
