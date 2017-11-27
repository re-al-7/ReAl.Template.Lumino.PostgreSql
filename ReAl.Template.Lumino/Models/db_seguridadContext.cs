using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReAl.Template.Lumino.Models
{    
    public partial class db_seguridadContext : DbContext
    {
        public db_seguridadContext(DbContextOptions<db_seguridadContext> options) :  
            base(options)  
        {  
        }
        
        public virtual DbSet<SegAplicaciones> SegAplicaciones { get; set; }
        public virtual DbSet<SegConfiguracion> SegConfiguracion { get; set; }
        public virtual DbSet<SegEstados> SegEstados { get; set; }
        public virtual DbSet<SegGlosas> SegGlosas { get; set; }
        public virtual DbSet<SegMensajes> SegMensajes { get; set; }
        public virtual DbSet<SegPaginas> SegPaginas { get; set; }
        public virtual DbSet<SegPersonas> SegPersonas { get; set; }
        public virtual DbSet<SegRoles> SegRoles { get; set; }
        public virtual DbSet<SegRolesPagina> SegRolesPagina { get; set; }
        public virtual DbSet<SegRolesTablaTransaccion> SegRolesTablaTransaccion { get; set; }
        public virtual DbSet<SegTablas> SegTablas { get; set; }
        public virtual DbSet<SegTransacciones> SegTransacciones { get; set; }
        public virtual DbSet<SegTransiciones> SegTransiciones { get; set; }
        public virtual DbSet<SegUsuarios> SegUsuarios { get; set; }
        public virtual DbSet<SegUsuariosRestriccion> SegUsuariosRestriccion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=db_seguridad;Username=postgres;Password=Desa2016");
            }
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SegAplicaciones>(entity =>
            {
                entity.HasKey(e => e.Idsap);

                entity.ToTable("seg_aplicaciones");

                entity.ForNpgsqlHasComment("Módulos del sistema");

                entity.HasIndex(e => e.Sigla)
                    .HasName("uk_sap_sigla")
                    .IsUnique();

                entity.Property(e => e.Idsap)
                    .HasColumnName("idsap")
                    .HasDefaultValueSql("nextval('seg_sap_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .ForNpgsqlHasComment("Descripción de aplicación");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que el registro fue creado");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha de última modificación del registro");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .ForNpgsqlHasComment("Nombre de Aplicación del Sistema");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .ForNpgsqlHasComment("Sigla de Aplicación del Sistema");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación en le registro");
            });

            modelBuilder.Entity<SegConfiguracion>(entity =>
            {
                entity.HasKey(e => e.Idscf);

                entity.ToTable("seg_configuracion");

                entity.ForNpgsqlHasComment("Almacena la configuracion posible por Tabla");

                entity.Property(e => e.Idscf)
                    .HasColumnName("idscf")
                    .HasDefaultValueSql("nextval('seg_scf_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Configuracion)
                    .IsRequired()
                    .HasColumnName("configuracion")
                    .ForNpgsqlHasComment("Definimos el nombre de la funcion a ejecutar");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod).HasColumnName("fecmod");

                entity.Property(e => e.Funcion)
                    .IsRequired()
                    .HasColumnName("funcion");

                entity.Property(e => e.Idsta).HasColumnName("idsta");

                entity.Property(e => e.Parametrosentrada)
                    .HasColumnName("parametrosentrada")
                    .HasDefaultValueSql("'[auth:json, datos:json]'::character varying")
                    .ForNpgsqlHasComment("parametros de entrada de la function, el parametro de entrada se reemplaza por un ? en el campo funcionsco; [*] significa todos los parametros");

                entity.Property(e => e.Parametrossalida)
                    .HasColumnName("parametrossalida")
                    .HasDefaultValueSql("'*'::character varying")
                    .ForNpgsqlHasComment("define los parametros de salida de la function , ejemplo idusuario,login o simplemente *");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdstaNavigation)
                    .WithMany(p => p.SegConfiguracion)
                    .HasForeignKey(d => d.Idsta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_scf_sta");
            });

            modelBuilder.Entity<SegEstados>(entity =>
            {
                entity.HasKey(e => e.Idses);

                entity.ToTable("seg_estados");

                entity.ForNpgsqlHasComment("Almacena todos los posibles estados que puedan poseer los registro de cada una de las tablas, deberá existir siempre el estado estado ELABORADO, que es el estado en el cual se encontrarán todos los registros una vez que hayan sido creados");

                entity.HasIndex(e => new { e.Idses, e.Idsta })
                    .HasName("uk_ses_sta")
                    .IsUnique();

                entity.HasIndex(e => new { e.Idsta, e.Estado })
                    .HasName("uk_ses_estado")
                    .IsUnique();

                entity.Property(e => e.Idses)
                    .HasColumnName("idses")
                    .HasDefaultValueSql("nextval('seg_ses_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .ForNpgsqlHasComment("Describe el el significado del Estado de esta tabla");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .ForNpgsqlHasComment("Estado de transición");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que el registro fue creado");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha de última modificación del registro");

                entity.Property(e => e.Idsta)
                    .HasColumnName("idsta")
                    .ForNpgsqlHasComment("ID de la tabla");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdstaNavigation)
                    .WithMany(p => p.SegEstados)
                    .HasForeignKey(d => d.Idsta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ses_sta");
            });

            modelBuilder.Entity<SegGlosas>(entity =>
            {
                entity.HasKey(e => e.Idsgl);

                entity.ToTable("seg_glosas");

                entity.ForNpgsqlHasComment("Registro de razones, justificaciones o motivos por los cuales se realiza una determinada transacción en un documento");

                entity.Property(e => e.Idsgl)
                    .HasColumnName("idsgl")
                    .HasDefaultValueSql("nextval('seg_sgl_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Glosa)
                    .IsRequired()
                    .HasColumnName("glosa")
                    .ForNpgsqlHasComment("Glosa que describe la acción realizada sobre el registro");

                entity.Property(e => e.Iddoc)
                    .HasColumnName("iddoc")
                    .ForNpgsqlHasComment("Identificador primario del documento al que hace referencia");

                entity.Property(e => e.Nombretabla)
                    .IsRequired()
                    .HasColumnName("nombretabla")
                    .ForNpgsqlHasComment("Nombre de la tabla donde se realiza la transacción");

                entity.Property(e => e.Transaccion)
                    .IsRequired()
                    .HasColumnName("transaccion")
                    .ForNpgsqlHasComment("Transacción que se realiza en la tabla");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.NombretablaNavigation)
                    .WithMany(p => p.SegGlosas)
                    .HasPrincipalKey(p => p.Nombretabla)
                    .HasForeignKey(d => d.Nombretabla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sgl_sta");
            });

            modelBuilder.Entity<SegMensajes>(entity =>
            {
                entity.HasKey(e => e.Idsme);

                entity.ToTable("seg_mensajes");

                entity.ForNpgsqlHasComment("Almacena los mensajes de error que se originan en la operación del sistema, se clasifica los mismos por el código de aplicación");

                entity.HasIndex(e => e.Aplicacionerror)
                    .HasName("uk_sme_aplicacionerror")
                    .IsUnique();

                entity.Property(e => e.Idsme)
                    .HasColumnName("idsme")
                    .HasDefaultValueSql("nextval('seg_sme_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario del registro");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasColumnName("accion")
                    .ForNpgsqlHasComment("Acción que se debe realizar para subsanar el error");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Aplicacionerror)
                    .IsRequired()
                    .HasColumnName("aplicacionerror")
                    .ForNpgsqlHasComment("Concatenación del número de error, con la aplicación a la que este pertenece");

                entity.Property(e => e.Causa)
                    .IsRequired()
                    .HasColumnName("causa")
                    .ForNpgsqlHasComment("Descripción detallada de por que se origina el error");

                entity.Property(e => e.Comentario)
                    .HasColumnName("comentario")
                    .ForNpgsqlHasComment("Comentario realizado acerca del error, puede ser utilizado por el desarrollador");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que el registro fue creado");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha de última modificación del registro");

                entity.Property(e => e.Idsap)
                    .HasColumnName("idsap")
                    .ForNpgsqlHasComment("Aplicación al que pertenece el mensaje");

                entity.Property(e => e.Origen)
                    .IsRequired()
                    .HasColumnName("origen")
                    .ForNpgsqlHasComment("Origen físico donde se ha originado el error");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .ForNpgsqlHasComment("Texto descriptivo del mensaje de error");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdsapNavigation)
                    .WithMany(p => p.SegMensajes)
                    .HasForeignKey(d => d.Idsap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sme_sap");
            });

            modelBuilder.Entity<SegPaginas>(entity =>
            {
                entity.HasKey(e => e.Idspg);

                entity.ToTable("seg_paginas");

                entity.ForNpgsqlHasComment("Contiene los nombres fisico de las todas las paginas del sistema, clasificadas por aplicacion, se valida que la extencion del nombre de pagina termine con los caracteres .aspx");

                entity.Property(e => e.Idspg)
                    .HasColumnName("idspg")
                    .HasDefaultValueSql("nextval('seg_spg_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .ForNpgsqlHasComment("Descripción de funcionalidad de la página");

                entity.Property(e => e.Metodo)
                    .HasColumnName("metodo")
                    .ForNpgsqlHasComment("Metodo de la pagina");
                
                entity.Property(e => e.Accion)
                    .HasColumnName("accion")
                    .ForNpgsqlHasComment("Accion de la pagina");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Icono)
                    .HasColumnName("icono")
                    .ForNpgsqlHasComment("icono de la página obtenidos de font awesome");

                entity.Property(e => e.Idsap)
                    .HasColumnName("idsap")
                    .ForNpgsqlHasComment("Identificador primario de aplicación a la que pertenece la página");

                entity.Property(e => e.Nivel)
                    .HasColumnName("nivel")
                    .ForNpgsqlHasComment("en que nivel se encuentra la página,");

                entity.Property(e => e.Nombremenu)
                    .HasColumnName("nombremenu")
                    .ForNpgsqlHasComment("Nombre del menu en el sistema");

                entity.Property(e => e.Paginapadre)
                    .HasColumnName("paginapadre")
                    .HasColumnType("numeric(3, 0)")
                    .ForNpgsqlHasComment("identificador del padre, si es null es padre");

                entity.Property(e => e.Prioridad)
                    .HasColumnName("prioridad")
                    .HasColumnType("numeric(22, 0)")
                    .HasDefaultValueSql("1")
                    .ForNpgsqlHasComment("En que prioridad se desplegara la pagina");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .ForNpgsqlHasComment("Sigla de la página, no debe existir repetidos");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdsapNavigation)
                    .WithMany(p => p.SegPaginas)
                    .HasForeignKey(d => d.Idsap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_spg_sap");
            });

            modelBuilder.Entity<SegPersonas>(entity =>
            {
                entity.HasKey(e => e.Idspe);

                entity.ToTable("seg_personas");

                entity.ForNpgsqlHasComment("Datos personales de usuarios del sistema");

                entity.HasIndex(e => e.Ci)
                    .HasName("uk_spe_ci")
                    .IsUnique();

                entity.HasIndex(e => e.Correo)
                    .HasName("uk_spe_correo")
                    .IsUnique();

                entity.Property(e => e.Idspe)
                    .HasColumnName("idspe")
                    .HasDefaultValueSql("nextval('seg_spe_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .ForNpgsqlHasComment("Apellidos de la persona");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasColumnName("ci")
                    .ForNpgsqlHasComment("Número de Identificación personal");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .ForNpgsqlHasComment("Correo electrónico. Sirve también como login del nuevo usuario para acceso al sistema");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .ForNpgsqlHasComment("Nombre de la persona que se registra");

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("sexo")
                    .HasDefaultValueSql("'M'::character varying")
                    .ForNpgsqlHasComment("Sexo de la persona. M: Masculino; F: Femenino");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");
            });

            modelBuilder.Entity<SegRoles>(entity =>
            {
                entity.HasKey(e => e.Idsro);

                entity.ToTable("seg_roles");

                entity.ForNpgsqlHasComment("Definicion de ROLES de operacion en el sistema que se asignan a los distintos usuarios");

                entity.Property(e => e.Idsro)
                    .HasColumnName("idsro")
                    .HasDefaultValueSql("nextval('seg_sro_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .ForNpgsqlHasComment("Descripción detallada del rol y su funcionalidad");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .ForNpgsqlHasComment("Sigla del rol");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");
            });

            modelBuilder.Entity<SegRolesPagina>(entity =>
            {
                entity.HasKey(e => e.Idsrp);

                entity.ToTable("seg_roles_pagina");

                entity.ForNpgsqlHasComment("Registra las diferentes paginas que son accesibles por un determinado rol");

                entity.Property(e => e.Idsrp)
                    .HasColumnName("idsrp")
                    .HasDefaultValueSql("nextval('seg_srp_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Idspg)
                    .HasColumnName("idspg")
                    .ForNpgsqlHasComment("Identificador primario de página que se asigna al rol de operación");

                entity.Property(e => e.Idsro)
                    .HasColumnName("idsro")
                    .ForNpgsqlHasComment("Identificador primario de rol al que se le asigna una página");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdspgNavigation)
                    .WithMany(p => p.SegRolesPagina)
                    .HasForeignKey(d => d.Idspg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_srp_spg");

                entity.HasOne(d => d.IdsroNavigation)
                    .WithMany(p => p.SegRolesPagina)
                    .HasForeignKey(d => d.Idsro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_srp_sro");
            });

            modelBuilder.Entity<SegRolesTablaTransaccion>(entity =>
            {
                entity.HasKey(e => e.Idstt);

                entity.ToTable("seg_roles_tabla_transaccion");

                entity.ForNpgsqlHasComment("Permite relacionar las tablas a las cuales tiene tiene acceso el rol y define tambien las transacciones de esa tabla que el usuario asignado a este rol puede realizar");

                entity.Property(e => e.Idstt)
                    .HasColumnName("idstt")
                    .HasDefaultValueSql("nextval('seg_stt_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Idsro)
                    .HasColumnName("idsro")
                    .ForNpgsqlHasComment("Identificador primario de rol de operación al que se asignan los permisos de ejecución");

                entity.Property(e => e.Idsta)
                    .HasColumnName("idsta")
                    .ForNpgsqlHasComment("Identificador primmario de tabla a la que tiene acceso el rol de operación");

                entity.Property(e => e.Idstr)
                    .HasColumnName("idstr")
                    .ForNpgsqlHasComment("Identificador primario de transacción que el rol de operación puede realizar en la tabla");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdsroNavigation)
                    .WithMany(p => p.SegRolesTablaTransaccion)
                    .HasForeignKey(d => d.Idsro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_stt_sro");

                entity.HasOne(d => d.Idst)
                    .WithMany(p => p.SegRolesTablaTransaccion)
                    .HasPrincipalKey(p => new { p.Idstr, p.Idsta })
                    .HasForeignKey(d => new { d.Idstr, d.Idsta })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_stt_str");
            });

            modelBuilder.Entity<SegTablas>(entity =>
            {
                entity.HasKey(e => e.Idsta);

                entity.ToTable("seg_tablas");

                entity.ForNpgsqlHasComment("Almacena las tablas del sistema y su respectiva descripcion");

                entity.HasIndex(e => e.Alias)
                    .HasName("uk_sta_alias")
                    .IsUnique();

                entity.HasIndex(e => e.Nombretabla)
                    .HasName("uk_sta_nombretabla")
                    .IsUnique();

                entity.Property(e => e.Idsta)
                    .HasColumnName("idsta")
                    .HasDefaultValueSql("nextval('seg_sta_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Afterrow)
                    .HasColumnName("afterrow")
                    .HasDefaultValueSql("false")
                    .ForNpgsqlHasComment("Indica si existirán RdN's en esta opción del disparador");

                entity.Property(e => e.Afterstatement)
                    .HasColumnName("afterstatement")
                    .HasDefaultValueSql("false")
                    .ForNpgsqlHasComment("Indica si existirán RdN's en esta opción del disparador");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasColumnName("alias")
                    .ForNpgsqlHasComment("Alias de tabla, identificador único de tabla se utiliza este valor para identificar a los procedimientos de la misma");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Beforerow)
                    .HasColumnName("beforerow")
                    .HasDefaultValueSql("true")
                    .ForNpgsqlHasComment("Indica si existirán RdN's en esta opción del disparador");

                entity.Property(e => e.Beforestatement)
                    .HasColumnName("beforestatement")
                    .HasDefaultValueSql("false")
                    .ForNpgsqlHasComment("Indica si existirán RdN's en esta opción del disparador");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .ForNpgsqlHasComment("Descripciòn de objetos que almacena la tabla");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que el registro fue creado");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha de última modificación del registro");

                entity.Property(e => e.Idsap)
                    .HasColumnName("idsap")
                    .ForNpgsqlHasComment("Identificador primario de aplicación a la que pertenece este registro");

                entity.Property(e => e.Nombretabla)
                    .IsRequired()
                    .HasColumnName("nombretabla")
                    .ForNpgsqlHasComment("Nombre de tabla");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdsapNavigation)
                    .WithMany(p => p.SegTablas)
                    .HasForeignKey(d => d.Idsap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sta_sap");
            });

            modelBuilder.Entity<SegTransacciones>(entity =>
            {
                entity.HasKey(e => e.Idstr);

                entity.ToTable("seg_transacciones");

                entity.ForNpgsqlHasComment("Almacena las TRANSACCIONES que pueden ser realizadas en una TABLA particular");

                entity.HasIndex(e => new { e.Idsta, e.Transaccion })
                    .HasName("uk_str_idSta_Transaccion")
                    .IsUnique();

                entity.HasIndex(e => new { e.Idstr, e.Idsta })
                    .HasName("uk_str_idstr_idSta")
                    .IsUnique();

                entity.Property(e => e.Idstr)
                    .HasColumnName("idstr")
                    .HasDefaultValueSql("nextval('seg_str_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .ForNpgsqlHasComment("Descripción de lo que la transacción realiza");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que el registro fue creado");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha de última modificación del registro");

                entity.Property(e => e.Idsta)
                    .HasColumnName("idsta")
                    .ForNpgsqlHasComment("Identificador primario de tabla a la que pertenece el estado");

                entity.Property(e => e.Sentencia)
                    .IsRequired()
                    .HasColumnName("sentencia")
                    .ForNpgsqlHasComment("Indica la sentencia en la que se realiza la transacción");

                entity.Property(e => e.Transaccion)
                    .IsRequired()
                    .HasColumnName("transaccion")
                    .ForNpgsqlHasComment("Nombre de transacción");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.IdstaNavigation)
                    .WithMany(p => p.SegTransacciones)
                    .HasForeignKey(d => d.Idsta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_str_sta");
            });

            modelBuilder.Entity<SegTransiciones>(entity =>
            {
                entity.HasKey(e => e.Idsts);

                entity.ToTable("seg_transiciones");

                entity.ForNpgsqlHasComment("Indica las TRANSICIONES entre ESTADOS que se definen para una determinada TABLA");

                entity.HasIndex(e => new { e.Idsta, e.Idsesini, e.Idstr, e.Idsesfin })
                    .HasName("uk_sts_idsta_estTraTrs")
                    .IsUnique();

                entity.Property(e => e.Idsts)
                    .HasColumnName("idsts")
                    .HasDefaultValueSql("nextval('seg_sts_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que el registro fue creado");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha de última modificación del registro");

                entity.Property(e => e.Idsesfin)
                    .HasColumnName("idsesfin")
                    .ForNpgsqlHasComment("Identificador primario de estado final de la transición");

                entity.Property(e => e.Idsesini)
                    .HasColumnName("idsesini")
                    .ForNpgsqlHasComment("Identificador primario de estado inicial");

                entity.Property(e => e.Idsta)
                    .HasColumnName("idsta")
                    .ForNpgsqlHasComment("Identificador primario de la tabla a la que pertenece el registro");

                entity.Property(e => e.Idstr)
                    .HasColumnName("idstr")
                    .ForNpgsqlHasComment("Identificador primario de transacción");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.HasOne(d => d.Idst)
                    .WithMany(p => p.SegTransiciones)
                    .HasPrincipalKey(p => new { p.Idstr, p.Idsta })
                    .HasForeignKey(d => new { d.Idstr, d.Idsta })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sts_str");
            });

            modelBuilder.Entity<SegUsuarios>(entity =>
            {
                entity.HasKey(e => e.Idsus);

                entity.ToTable("seg_usuarios");

                entity.ForNpgsqlHasComment("Almacena la informacion de usuarios del sistema SGIBS que poseen conexion a sus distintos modulos");

                entity.Property(e => e.Idsus)
                    .HasColumnName("idsus")
                    .HasDefaultValueSql("nextval('seg_sus_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Idspe)
                    .HasColumnName("idspe")
                    .ForNpgsqlHasComment("Identificador primario de registro de datos personales de la persona");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .ForNpgsqlHasComment("Nombre de usuario en el sistema");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .ForNpgsqlHasComment("contraseña encriptada en hash");

                entity.Property(e => e.TokenPassword)
                    .HasColumnName("token_password")
                    .ForNpgsqlHasComment("'Token para restablecer la contraseña'");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.Property(e => e.Vigente)
                    .HasColumnName("vigente")
                    .HasDefaultValueSql("1")
                    .ForNpgsqlHasComment("Indica si el usuario está vigente en el sistema. 1: Está Vigente; 0 No vigente");

                entity.HasOne(d => d.IdspeNavigation)
                    .WithMany(p => p.SegUsuarios)
                    .HasForeignKey(d => d.Idspe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sus_pda");
            });

            modelBuilder.Entity<SegUsuariosRestriccion>(entity =>
            {
                entity.HasKey(e => e.Idsur);

                entity.ToTable("seg_usuarios_restriccion");

                entity.ForNpgsqlHasComment("Registro de usuarios del sistema, se definen aqui el o los distintos roles de operacion que posee un usuario, tambien el nivel de restriccion para cada uno de los roles definidos, los niveles de restriccion se dan a nivel de institucion, gerencia administrativa y unidad ejecutora");

                entity.Property(e => e.Idsur)
                    .HasColumnName("idsur")
                    .HasDefaultValueSql("nextval('seg_sur_seq'::regclass)")
                    .ForNpgsqlHasComment("Identificador primario de registro");

                entity.Property(e => e.Apiestado)
                    .IsRequired()
                    .HasColumnName("apiestado")
                    .HasDefaultValueSql("'ELABORADO'::character varying")
                    .ForNpgsqlHasComment("Estado actual del registro");

                entity.Property(e => e.Apitransaccion)
                    .IsRequired()
                    .HasColumnName("apitransaccion")
                    .HasDefaultValueSql("'CREAR'::character varying")
                    .ForNpgsqlHasComment("Ultima transacción realizada en el registro");

                entity.Property(e => e.Feccre)
                    .HasColumnName("feccre")
                    .HasDefaultValueSql("now()")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la creación del registro");

                entity.Property(e => e.Fecmod)
                    .HasColumnName("fecmod")
                    .ForNpgsqlHasComment("Fecha en la que se realizó la última modificación del registro");

                entity.Property(e => e.Idsro)
                    .HasColumnName("idsro")
                    .ForNpgsqlHasComment("Identificador primario de rol de operación que se asigna al usuario");

                entity.Property(e => e.Idsus)
                    .HasColumnName("idsus")
                    .ForNpgsqlHasComment("Identificador primario de usuario al que se le asigna el rol");

                entity.Property(e => e.Restriccion)
                    .IsRequired()
                    .HasColumnName("restriccion")
                    .HasDefaultValueSql("'E'::character varying")
                    .ForNpgsqlHasComment("Indica el nivel de restricción a la que está sujeto el rol de operación cuando se accede al sistema con el mismo. N: Ninguna Restricción;E: Evento;U: Personal o Usuario");

                entity.Property(e => e.Rolactivo)
                    .HasColumnName("rolactivo")
                    .ForNpgsqlHasComment("Si el usuartio tiene más de un rol, este campo indica si este rol es el rol activo para realizar operaciones en la aplicación");

                entity.Property(e => e.Usucre)
                    .IsRequired()
                    .HasColumnName("usucre")
                    .HasDefaultValueSql("\"current_user\"()")
                    .ForNpgsqlHasComment("Usuario que realizó la creación del registro");

                entity.Property(e => e.Usumod)
                    .HasColumnName("usumod")
                    .ForNpgsqlHasComment("Usuario que realizó la última modificación del registro");

                entity.Property(e => e.Vigente)
                    .HasColumnName("vigente")
                    .HasDefaultValueSql("1")
                    .ForNpgsqlHasComment("Indica si el rol de operación está vigente. Este valor tiene mayor eso específico que la fecha de vigencia, si este campo indica 0 (No vigente) no importa si la fecha de vigencia si lo está, el rol no está vigente. 1: Vigente; 0 No vigente");

                entity.HasOne(d => d.IdsroNavigation)
                    .WithMany(p => p.SegUsuariosRestriccion)
                    .HasForeignKey(d => d.Idsro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sur_sro");

                entity.HasOne(d => d.IdsusNavigation)
                    .WithMany(p => p.SegUsuariosRestriccion)
                    .HasForeignKey(d => d.Idsus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sur_sus");
            });

            modelBuilder.HasSequence("seg_sap_seq");

            modelBuilder.HasSequence("seg_scf_seq");

            modelBuilder.HasSequence("seg_ses_seq");

            modelBuilder.HasSequence("seg_sgl_seq");

            modelBuilder.HasSequence("seg_sme_seq");

            modelBuilder.HasSequence("seg_spe_seq");

            modelBuilder.HasSequence("seg_spg_seq");

            modelBuilder.HasSequence("seg_sro_seq");

            modelBuilder.HasSequence("seg_srp_seq");

            modelBuilder.HasSequence("seg_sta_seq");

            modelBuilder.HasSequence("seg_str_seq");

            modelBuilder.HasSequence("seg_sts_seq");

            modelBuilder.HasSequence("seg_stt_seq");

            modelBuilder.HasSequence("seg_sur_seq");

            modelBuilder.HasSequence("seg_sus_seq");
        }
    }
}
