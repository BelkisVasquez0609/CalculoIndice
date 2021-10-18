//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalculoIndice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asignatura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asignatura()
        {
            this.Calificacion = new HashSet<Calificacion>();
            this.Asignatura1 = new HashSet<Asignatura>();
            this.Asignatura11 = new HashSet<Asignatura>();
        }
    
        public int AsignaturaId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Credito { get; set; }
        public Nullable<int> PreRequisitos { get; set; }
        public Nullable<int> CoRequisitos { get; set; }
        public Nullable<int> ProfesoresId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignatura> Asignatura1 { get; set; }
        public virtual Asignatura Asignatura2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignatura> Asignatura11 { get; set; }
        public virtual Asignatura Asignatura3 { get; set; }
        public virtual Profesores Profesores { get; set; }
    }
}
