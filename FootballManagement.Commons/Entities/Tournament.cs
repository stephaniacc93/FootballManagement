//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;
using System.Runtime.Serialization;


namespace FootballManagement.Commons.Entities
{
    [DataContract]
    public partial class Tournament
    {
        #region Primitive Properties
    [DataMember]
    		[Required]
    	    public virtual int Id
        {
            get;
            set;
        }
    [DataMember]
    		[Required]
    	    public virtual string Name
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual ICollection<Match> Matches
        {
            get
            {
                if (_matches == null)
                {
                    var newCollection = new FixupCollection<Match>();
                    newCollection.CollectionChanged += FixupMatches;
                    _matches = newCollection;
                }
                return _matches;
            }
            set
            {
                if (!ReferenceEquals(_matches, value))
                {
                    var previousValue = _matches as FixupCollection<Match>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMatches;
                    }
                    _matches = value;
                    var newValue = value as FixupCollection<Match>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMatches;
                    }
                }
            }
        }
        private ICollection<Match> _matches;
    
        public virtual ICollection<Team> Teams
        {
            get
            {
                if (_teams == null)
                {
                    var newCollection = new FixupCollection<Team>();
                    newCollection.CollectionChanged += FixupTeams;
                    _teams = newCollection;
                }
                return _teams;
            }
            set
            {
                if (!ReferenceEquals(_teams, value))
                {
                    var previousValue = _teams as FixupCollection<Team>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTeams;
                    }
                    _teams = value;
                    var newValue = value as FixupCollection<Team>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTeams;
                    }
                }
            }
        }
        private ICollection<Team> _teams;
    
        public virtual ICollection<Referee> Referees
        {
            get
            {
                if (_referees == null)
                {
                    var newCollection = new FixupCollection<Referee>();
                    newCollection.CollectionChanged += FixupReferees;
                    _referees = newCollection;
                }
                return _referees;
            }
            set
            {
                if (!ReferenceEquals(_referees, value))
                {
                    var previousValue = _referees as FixupCollection<Referee>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupReferees;
                    }
                    _referees = value;
                    var newValue = value as FixupCollection<Referee>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupReferees;
                    }
                }
            }
        }
        private ICollection<Referee> _referees;

        #endregion

        #region Association Fixup
    
        private void FixupMatches(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Match item in e.NewItems)
                {
                    item.Tournament = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Match item in e.OldItems)
                {
                    if (ReferenceEquals(item.Tournament, this))
                    {
                        item.Tournament = null;
                    }
                }
            }
        }
    
        private void FixupTeams(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Team item in e.NewItems)
                {
                    item.Tournament = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Team item in e.OldItems)
                {
                    if (ReferenceEquals(item.Tournament, this))
                    {
                        item.Tournament = null;
                    }
                }
            }
        }
    
        private void FixupReferees(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Referee item in e.NewItems)
                {
                    if (!item.Tournaments.Contains(this))
                    {
                        item.Tournaments.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Referee item in e.OldItems)
                {
                    if (item.Tournaments.Contains(this))
                    {
                        item.Tournaments.Remove(this);
                    }
                }
            }
        }

        #endregion

    }
}
