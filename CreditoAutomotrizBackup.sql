PGDMP                         z            CreditoAutomotriz    14.4    14.4 D    L           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            M           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            N           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            O           1262    16394    CreditoAutomotriz    DATABASE     q   CREATE DATABASE "CreditoAutomotriz" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Ecuador.1252';
 #   DROP DATABASE "CreditoAutomotriz";
                postgres    false            �            1259    16395    cliente_id_seq    SEQUENCE     �   CREATE SEQUENCE public.cliente_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.cliente_id_seq;
       public          postgres    false            �            1259    16396    Cliente    TABLE     v  CREATE TABLE public."Cliente" (
    "Id" integer DEFAULT nextval('public.cliente_id_seq'::regclass) NOT NULL,
    "Identificacion" character varying(10) NOT NULL,
    "Nombres" character varying(100) NOT NULL,
    "Apellidos" character varying(200) NOT NULL,
    "Edad" integer NOT NULL,
    "FechaNacimiento" date NOT NULL,
    "Direccion" character varying(150) NOT NULL,
    "EstadoCivil" character varying(50) NOT NULL,
    "IdentificacionConyugue" character varying(10) NOT NULL,
    "NombreConyugue" character varying(200) NOT NULL,
    "SujetoCredito" boolean DEFAULT false,
    "Telefono" character varying(9) NOT NULL
);
    DROP TABLE public."Cliente";
       public         heap    postgres    false    209            �            1259    16405    clientepatio_id_seq    SEQUENCE     �   CREATE SEQUENCE public.clientepatio_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.clientepatio_id_seq;
       public          postgres    false            �            1259    16406    ClientePatio    TABLE     �   CREATE TABLE public."ClientePatio" (
    "Id" integer DEFAULT nextval('public.clientepatio_id_seq'::regclass) NOT NULL,
    "IdCliente" integer NOT NULL,
    "IdPatio" integer NOT NULL,
    "FechaAsignacion" date NOT NULL
);
 "   DROP TABLE public."ClientePatio";
       public         heap    postgres    false    211            �            1259    16415    ejecutivo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ejecutivo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.ejecutivo_id_seq;
       public          postgres    false            �            1259    16416 	   Ejecutivo    TABLE     �  CREATE TABLE public."Ejecutivo" (
    "Id" integer DEFAULT nextval('public.ejecutivo_id_seq'::regclass) NOT NULL,
    "Identificacion" character varying(10) NOT NULL,
    "Nombres" character varying(100) NOT NULL,
    "Apellidos" character varying(200) NOT NULL,
    "Direccion" character varying(150) NOT NULL,
    "TelefonoConvencional" character varying(9),
    "Celular" character varying(10) NOT NULL,
    "IdPatio" integer,
    "Edad" integer NOT NULL
);
    DROP TABLE public."Ejecutivo";
       public         heap    postgres    false    213            �            1259    16424    marca_id_seq    SEQUENCE     �   CREATE SEQUENCE public.marca_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.marca_id_seq;
       public          postgres    false            �            1259    16425    Marca    TABLE     �   CREATE TABLE public."Marca" (
    "Id" integer DEFAULT nextval('public.marca_id_seq'::regclass) NOT NULL,
    "Nombre" character varying(100) NOT NULL
);
    DROP TABLE public."Marca";
       public         heap    postgres    false    215            �            1259    16432    patio_id_seq    SEQUENCE     �   CREATE SEQUENCE public.patio_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.patio_id_seq;
       public          postgres    false            �            1259    16433    Patio    TABLE     "  CREATE TABLE public."Patio" (
    "Id" integer DEFAULT nextval('public.patio_id_seq'::regclass) NOT NULL,
    "Nombre" character varying(100) NOT NULL,
    "Direccion" character varying(200) NOT NULL,
    "Telefono" character varying(9) NOT NULL,
    "NumeroPuntoVenta" integer NOT NULL
);
    DROP TABLE public."Patio";
       public         heap    postgres    false    217            �            1259    16440    solicitudcredito_id_seq    SEQUENCE     �   CREATE SEQUENCE public.solicitudcredito_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.solicitudcredito_id_seq;
       public          postgres    false            �            1259    16441    SolicitudCredito    TABLE     �  CREATE TABLE public."SolicitudCredito" (
    "Id" integer DEFAULT nextval('public.solicitudcredito_id_seq'::regclass) NOT NULL,
    "IdCliente" integer NOT NULL,
    "IdPatio" integer NOT NULL,
    "IdVehiculo" integer NOT NULL,
    "FechaElaboracion" date NOT NULL,
    "MesesPlazo" integer NOT NULL,
    "Cuotas" money NOT NULL,
    "Entrada" money NOT NULL,
    "IdEjecutivo" integer NOT NULL,
    "Observaciones" character varying(255)
);
 &   DROP TABLE public."SolicitudCredito";
       public         heap    postgres    false    219            �            1259    24639    TrackingSolicitud    TABLE     �   CREATE TABLE public."TrackingSolicitud" (
    "Id" integer NOT NULL,
    "IdSolicitud" integer NOT NULL,
    "FechaActualizacion" date NOT NULL,
    "Estado" character varying(50) NOT NULL
);
 '   DROP TABLE public."TrackingSolicitud";
       public         heap    postgres    false            �            1259    24638    TrackingSolicitud_Id_seq    SEQUENCE     �   ALTER TABLE public."TrackingSolicitud" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."TrackingSolicitud_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    224            �            1259    16452    vehiculo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.vehiculo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.vehiculo_id_seq;
       public          postgres    false            �            1259    16453    Vehiculo    TABLE     �  CREATE TABLE public."Vehiculo" (
    "Id" integer DEFAULT nextval('public.vehiculo_id_seq'::regclass) NOT NULL,
    "Placa" character varying(25) NOT NULL,
    "Modelo" character varying(50) NOT NULL,
    "NroChasis" integer NOT NULL,
    "IdMarca" integer NOT NULL,
    "Tipo" character varying(25),
    "Cilindraje" numeric(10,2) NOT NULL,
    "Avaluo" money NOT NULL,
    "Anio" integer NOT NULL
);
    DROP TABLE public."Vehiculo";
       public         heap    postgres    false    221            ;          0    16396    Cliente 
   TABLE DATA           �   COPY public."Cliente" ("Id", "Identificacion", "Nombres", "Apellidos", "Edad", "FechaNacimiento", "Direccion", "EstadoCivil", "IdentificacionConyugue", "NombreConyugue", "SujetoCredito", "Telefono") FROM stdin;
    public          postgres    false    210   W       =          0    16406    ClientePatio 
   TABLE DATA           Y   COPY public."ClientePatio" ("Id", "IdCliente", "IdPatio", "FechaAsignacion") FROM stdin;
    public          postgres    false    212    W       ?          0    16416 	   Ejecutivo 
   TABLE DATA           �   COPY public."Ejecutivo" ("Id", "Identificacion", "Nombres", "Apellidos", "Direccion", "TelefonoConvencional", "Celular", "IdPatio", "Edad") FROM stdin;
    public          postgres    false    214   =W       A          0    16425    Marca 
   TABLE DATA           1   COPY public."Marca" ("Id", "Nombre") FROM stdin;
    public          postgres    false    216   ZW       C          0    16433    Patio 
   TABLE DATA           ^   COPY public."Patio" ("Id", "Nombre", "Direccion", "Telefono", "NumeroPuntoVenta") FROM stdin;
    public          postgres    false    218   wW       E          0    16441    SolicitudCredito 
   TABLE DATA           �   COPY public."SolicitudCredito" ("Id", "IdCliente", "IdPatio", "IdVehiculo", "FechaElaboracion", "MesesPlazo", "Cuotas", "Entrada", "IdEjecutivo", "Observaciones") FROM stdin;
    public          postgres    false    220   �W       I          0    24639    TrackingSolicitud 
   TABLE DATA           b   COPY public."TrackingSolicitud" ("Id", "IdSolicitud", "FechaActualizacion", "Estado") FROM stdin;
    public          postgres    false    224   �W       G          0    16453    Vehiculo 
   TABLE DATA           }   COPY public."Vehiculo" ("Id", "Placa", "Modelo", "NroChasis", "IdMarca", "Tipo", "Cilindraje", "Avaluo", "Anio") FROM stdin;
    public          postgres    false    222   X       P           0    0    TrackingSolicitud_Id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."TrackingSolicitud_Id_seq"', 1, true);
          public          postgres    false    223            Q           0    0    cliente_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.cliente_id_seq', 1, true);
          public          postgres    false    209            R           0    0    clientepatio_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.clientepatio_id_seq', 1, true);
          public          postgres    false    211            S           0    0    ejecutivo_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.ejecutivo_id_seq', 1, true);
          public          postgres    false    213            T           0    0    marca_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.marca_id_seq', 1, true);
          public          postgres    false    215            U           0    0    patio_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.patio_id_seq', 1, true);
          public          postgres    false    217            V           0    0    solicitudcredito_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.solicitudcredito_id_seq', 1, true);
          public          postgres    false    219            W           0    0    vehiculo_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.vehiculo_id_seq', 1, true);
          public          postgres    false    221            �           2606    16403    Cliente PK_Cliente 
   CONSTRAINT     V   ALTER TABLE ONLY public."Cliente"
    ADD CONSTRAINT "PK_Cliente" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Cliente" DROP CONSTRAINT "PK_Cliente";
       public            postgres    false    210            �           2606    16411    ClientePatio PK_ClientePatio 
   CONSTRAINT     `   ALTER TABLE ONLY public."ClientePatio"
    ADD CONSTRAINT "PK_ClientePatio" PRIMARY KEY ("Id");
 J   ALTER TABLE ONLY public."ClientePatio" DROP CONSTRAINT "PK_ClientePatio";
       public            postgres    false    212            �           2606    16421    Ejecutivo PK_Ejecutivo 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Ejecutivo"
    ADD CONSTRAINT "PK_Ejecutivo" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Ejecutivo" DROP CONSTRAINT "PK_Ejecutivo";
       public            postgres    false    214            �           2606    16430    Marca PK_Marca 
   CONSTRAINT     R   ALTER TABLE ONLY public."Marca"
    ADD CONSTRAINT "PK_Marca" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Marca" DROP CONSTRAINT "PK_Marca";
       public            postgres    false    216            �           2606    16438    Patio PK_Patio 
   CONSTRAINT     R   ALTER TABLE ONLY public."Patio"
    ADD CONSTRAINT "PK_Patio" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Patio" DROP CONSTRAINT "PK_Patio";
       public            postgres    false    218            �           2606    16446 $   SolicitudCredito PK_SolicitudCredito 
   CONSTRAINT     h   ALTER TABLE ONLY public."SolicitudCredito"
    ADD CONSTRAINT "PK_SolicitudCredito" PRIMARY KEY ("Id");
 R   ALTER TABLE ONLY public."SolicitudCredito" DROP CONSTRAINT "PK_SolicitudCredito";
       public            postgres    false    220            �           2606    32779 &   TrackingSolicitud PK_TrackingSolicitud 
   CONSTRAINT     j   ALTER TABLE ONLY public."TrackingSolicitud"
    ADD CONSTRAINT "PK_TrackingSolicitud" PRIMARY KEY ("Id");
 T   ALTER TABLE ONLY public."TrackingSolicitud" DROP CONSTRAINT "PK_TrackingSolicitud";
       public            postgres    false    224            �           2606    16458    Vehiculo PK_Vehiculo 
   CONSTRAINT     X   ALTER TABLE ONLY public."Vehiculo"
    ADD CONSTRAINT "PK_Vehiculo" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Vehiculo" DROP CONSTRAINT "PK_Vehiculo";
       public            postgres    false    222            �           1259    16404 
   cliente_pk    INDEX     G   CREATE UNIQUE INDEX cliente_pk ON public."Cliente" USING btree ("Id");
    DROP INDEX public.cliente_pk;
       public            postgres    false    210            �           1259    16414    clienteclientepatio_fk    INDEX     X   CREATE INDEX clienteclientepatio_fk ON public."ClientePatio" USING btree ("IdCliente");
 *   DROP INDEX public.clienteclientepatio_fk;
       public            postgres    false    212            �           1259    16412    clientepatio_pk    INDEX     Q   CREATE UNIQUE INDEX clientepatio_pk ON public."ClientePatio" USING btree ("Id");
 #   DROP INDEX public.clientepatio_pk;
       public            postgres    false    212            �           1259    16448    clientesolicitudcredito_fk    INDEX     `   CREATE INDEX clientesolicitudcredito_fk ON public."SolicitudCredito" USING btree ("IdCliente");
 .   DROP INDEX public.clientesolicitudcredito_fk;
       public            postgres    false    220            �           1259    16422    ejecutivo_pk    INDEX     K   CREATE UNIQUE INDEX ejecutivo_pk ON public."Ejecutivo" USING btree ("Id");
     DROP INDEX public.ejecutivo_pk;
       public            postgres    false    214            �           1259    16451    ejecutivosolicitudcredito_fk    INDEX     d   CREATE INDEX ejecutivosolicitudcredito_fk ON public."SolicitudCredito" USING btree ("IdEjecutivo");
 0   DROP INDEX public.ejecutivosolicitudcredito_fk;
       public            postgres    false    220            �           1259    16431    marca_pk    INDEX     C   CREATE UNIQUE INDEX marca_pk ON public."Marca" USING btree ("Id");
    DROP INDEX public.marca_pk;
       public            postgres    false    216            �           1259    16460    marcavehiculo_fk    INDEX     L   CREATE INDEX marcavehiculo_fk ON public."Vehiculo" USING btree ("IdMarca");
 $   DROP INDEX public.marcavehiculo_fk;
       public            postgres    false    222            �           1259    16439    patio_pk    INDEX     C   CREATE UNIQUE INDEX patio_pk ON public."Patio" USING btree ("Id");
    DROP INDEX public.patio_pk;
       public            postgres    false    218            �           1259    16413    patioclientepatio_fk    INDEX     T   CREATE INDEX patioclientepatio_fk ON public."ClientePatio" USING btree ("IdPatio");
 (   DROP INDEX public.patioclientepatio_fk;
       public            postgres    false    212            �           1259    16423    patioejecutivo_fk    INDEX     N   CREATE INDEX patioejecutivo_fk ON public."Ejecutivo" USING btree ("IdPatio");
 %   DROP INDEX public.patioejecutivo_fk;
       public            postgres    false    214            �           1259    16449    patiosolicitudcredito_fk    INDEX     \   CREATE INDEX patiosolicitudcredito_fk ON public."SolicitudCredito" USING btree ("IdPatio");
 ,   DROP INDEX public.patiosolicitudcredito_fk;
       public            postgres    false    220            �           1259    16447    solicitudcredito_pk    INDEX     Y   CREATE UNIQUE INDEX solicitudcredito_pk ON public."SolicitudCredito" USING btree ("Id");
 '   DROP INDEX public.solicitudcredito_pk;
       public            postgres    false    220            �           1259    16459    vehiculo_pk    INDEX     I   CREATE UNIQUE INDEX vehiculo_pk ON public."Vehiculo" USING btree ("Id");
    DROP INDEX public.vehiculo_pk;
       public            postgres    false    222            �           1259    16450    vehiculosolicitudcredito_fk    INDEX     b   CREATE INDEX vehiculosolicitudcredito_fk ON public."SolicitudCredito" USING btree ("IdVehiculo");
 /   DROP INDEX public.vehiculosolicitudcredito_fk;
       public            postgres    false    220            �           2606    16461 *   ClientePatio FK_ClienteP_ClienteCl_Cliente    FK CONSTRAINT     �   ALTER TABLE ONLY public."ClientePatio"
    ADD CONSTRAINT "FK_ClienteP_ClienteCl_Cliente" FOREIGN KEY ("IdCliente") REFERENCES public."Cliente"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 X   ALTER TABLE ONLY public."ClientePatio" DROP CONSTRAINT "FK_ClienteP_ClienteCl_Cliente";
       public          postgres    false    3208    210    212            �           2606    16466 (   ClientePatio FK_ClienteP_PatioClie_Patio    FK CONSTRAINT     �   ALTER TABLE ONLY public."ClientePatio"
    ADD CONSTRAINT "FK_ClienteP_PatioClie_Patio" FOREIGN KEY ("IdPatio") REFERENCES public."Patio"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 V   ALTER TABLE ONLY public."ClientePatio" DROP CONSTRAINT "FK_ClienteP_PatioClie_Patio";
       public          postgres    false    3223    218    212            �           2606    16471 %   Ejecutivo FK_Ejecutiv_PatioEjec_Patio    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ejecutivo"
    ADD CONSTRAINT "FK_Ejecutiv_PatioEjec_Patio" FOREIGN KEY ("IdPatio") REFERENCES public."Patio"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 S   ALTER TABLE ONLY public."Ejecutivo" DROP CONSTRAINT "FK_Ejecutiv_PatioEjec_Patio";
       public          postgres    false    214    3223    218            �           2606    16476 .   SolicitudCredito FK_Solicitu_ClienteSo_Cliente    FK CONSTRAINT     �   ALTER TABLE ONLY public."SolicitudCredito"
    ADD CONSTRAINT "FK_Solicitu_ClienteSo_Cliente" FOREIGN KEY ("IdCliente") REFERENCES public."Cliente"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 \   ALTER TABLE ONLY public."SolicitudCredito" DROP CONSTRAINT "FK_Solicitu_ClienteSo_Cliente";
       public          postgres    false    220    210    3208            �           2606    16481 /   SolicitudCredito FK_Solicitu_Ejecutivo_Ejecutiv    FK CONSTRAINT     �   ALTER TABLE ONLY public."SolicitudCredito"
    ADD CONSTRAINT "FK_Solicitu_Ejecutivo_Ejecutiv" FOREIGN KEY ("IdEjecutivo") REFERENCES public."Ejecutivo"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 ]   ALTER TABLE ONLY public."SolicitudCredito" DROP CONSTRAINT "FK_Solicitu_Ejecutivo_Ejecutiv";
       public          postgres    false    220    3216    214            �           2606    16486 ,   SolicitudCredito FK_Solicitu_PatioSoli_Patio    FK CONSTRAINT     �   ALTER TABLE ONLY public."SolicitudCredito"
    ADD CONSTRAINT "FK_Solicitu_PatioSoli_Patio" FOREIGN KEY ("IdPatio") REFERENCES public."Patio"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 Z   ALTER TABLE ONLY public."SolicitudCredito" DROP CONSTRAINT "FK_Solicitu_PatioSoli_Patio";
       public          postgres    false    3223    220    218            �           2606    16491 /   SolicitudCredito FK_Solicitu_VehiculoS_Vehiculo    FK CONSTRAINT     �   ALTER TABLE ONLY public."SolicitudCredito"
    ADD CONSTRAINT "FK_Solicitu_VehiculoS_Vehiculo" FOREIGN KEY ("IdVehiculo") REFERENCES public."Vehiculo"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 ]   ALTER TABLE ONLY public."SolicitudCredito" DROP CONSTRAINT "FK_Solicitu_VehiculoS_Vehiculo";
       public          postgres    false    3233    220    222            �           2606    24644 0   TrackingSolicitud FK_Solicitud_TrackingSolicitud    FK CONSTRAINT     �   ALTER TABLE ONLY public."TrackingSolicitud"
    ADD CONSTRAINT "FK_Solicitud_TrackingSolicitud" FOREIGN KEY ("IdSolicitud") REFERENCES public."SolicitudCredito"("Id");
 ^   ALTER TABLE ONLY public."TrackingSolicitud" DROP CONSTRAINT "FK_Solicitud_TrackingSolicitud";
       public          postgres    false    3226    224    220            �           2606    16496 $   Vehiculo FK_Vehiculo_MarcaVehi_Marca    FK CONSTRAINT     �   ALTER TABLE ONLY public."Vehiculo"
    ADD CONSTRAINT "FK_Vehiculo_MarcaVehi_Marca" FOREIGN KEY ("IdMarca") REFERENCES public."Marca"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 R   ALTER TABLE ONLY public."Vehiculo" DROP CONSTRAINT "FK_Vehiculo_MarcaVehi_Marca";
       public          postgres    false    216    222    3220            ;      x������ � �      =      x������ � �      ?      x������ � �      A      x������ � �      C   C   x�3�H,��W()M-JN,�t,�S�J��/JT�Tp.�LLJ���4022�017��425������� ,r`      E      x������ � �      I      x������ � �      G      x������ � �     