import {ReactNode} from "react";

interface SectionProps {
  id?: string;
  title: string;
  children: ReactNode | ReactNode[];
}

export default function Section({id, title, children}: SectionProps)
{
  return (
    <section id={id} key={id}>
      <h3>{title}</h3>
      {children}
    </section>
  );
}